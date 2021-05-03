/* BoutDuTunnel Copyright (c) 2006-2021 Sebastien Lebreton

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Bdt.GuiClient.Resources;
using Bdt.Shared.Service;
using Bdt.Shared.Request;
using Bdt.Shared.Response;

namespace Bdt.GuiClient.Forms
{
	public partial class AdminForm : Form
	{
		private readonly ITunnel _tunnel;
		private readonly int _sid;
		private readonly string _sidhex;
		private Session _currentsession;

		public AdminForm(ITunnel tunnel, int sid)
		{
			InitializeComponent();
			_tunnel = tunnel;
			_sid = sid;
			_sidhex = _sid.ToString("x");
		}

		private void AdminForm_Load(object sender, EventArgs e)
		{
			RefreshSessions();
		}

		private static void HandleResponse(IMinimalResponse response)
		{
			if (!response.Success)
				MessageBox.Show(response.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void RefreshSessions()
		{
			try
			{
				BtRefresh.Enabled = BtClose.Enabled = false;
				UseWaitCursor = true;

				Application.DoEvents();
				var response = _tunnel.Monitor(new SessionContextRequest(_sid));
				if (response.Success)
				{
					_currentsession = default(Session);
					SessionsBindingSource.DataSource = response.Sessions;
					SessionsBindingSource.ResetBindings(false);

					if (Sessions.SelectedRows.Count > 0)
					{
						_currentsession = (Session)Sessions.SelectedRows[0].DataBoundItem;
						ConnectionsBindingSource.DataSource = _currentsession.Connections;
					}
					else
						ConnectionsBindingSource.DataSource = null;
				}
				else
					HandleResponse(response);
			}
			finally
			{
				BtRefresh.Enabled = BtClose.Enabled = true;
				UseWaitCursor = false;
			}
		}

		private void Sessions_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.RowIndex >= Sessions.Rows.Count)
				return;

			_currentsession = (Session)Sessions.Rows[e.RowIndex].DataBoundItem;
			ConnectionsBindingSource.DataSource = _currentsession.Connections;
		}

		private void KillSessionItem_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in Sessions.SelectedRows)
			{
				var session = (Session)row.DataBoundItem;

				if (!int.TryParse(session.Sid, System.Globalization.NumberStyles.HexNumber, null, out var targetsid))
					continue;

				if (targetsid == _sid)
					MessageBox.Show(Strings.ADMINFORM_KILL_OWN_SESSION, string.Empty, MessageBoxButtons.OK,
						MessageBoxIcon.Warning);
				else
				{
					IMinimalResponse response = _tunnel.KillSession(new KillSessionRequest(targetsid, _sid));
					if (!response.Success)
						HandleResponse(response);
				}
			}

			RefreshSessions();
		}

		private void KillConnectionItem_Click(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in Connections.SelectedRows)
			{
				var connection = (Connection)row.DataBoundItem;

				if (int.TryParse(_currentsession.Sid, System.Globalization.NumberStyles.HexNumber, null, out var targetsid)
				    && int.TryParse(connection.Cid, System.Globalization.NumberStyles.HexNumber, null, out var targetcid))
				{
					IMinimalResponse response = _tunnel.KillConnection(new KillConnectionRequest(targetsid, _sid, targetcid));
					if (!response.Success)
						HandleResponse(response);
				}
			}

			RefreshSessions();
		}

		private void Refresh_Click(object sender, EventArgs e)
		{
			RefreshSessions();
		}

		private void SessionsMenu_Opening(object sender, CancelEventArgs e)
		{
			e.Cancel = Sessions.SelectedRows.Count == 0;
		}

		private void ConnectionsMenu_Opening(object sender, CancelEventArgs e)
		{
			e.Cancel = Connections.SelectedRows.Count == 0;
		}

		private void Sessions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			var session = (Session)Sessions.Rows[e.RowIndex].DataBoundItem;
			if (session.Sid == _sidhex)
				e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
		}
	}
}
