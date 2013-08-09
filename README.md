Bdtunnel
========

BoutDuTunnel is a useful software for users behind restrictive firewalls and is intended to use network services that normally you cannot. You need a server located outside the firewalled area (for example your computer with permanent connection at home) which have full access to the Internet. BoutDuTunnel is then able to create virtual connections tunnelled in HTTP requests. All data is scrambled to preserve anonymity.

BoutDuTunnel is compatible with HTTP proxy servers, even if they use NTLM authentication (like ISA Server) and even if they prohibit the "connect method". BoutDuTunnel client acts as a Socks Server (v4, v4a and v5 are supported) and is compatible with static forwards. Several communication protocols are supported (a Built-in HTTP Server is available on the server side):

- HTTP binary (good performance, good compatibility, proxy compatible)
- HTTP SOAP (maximum compatibility, proxy compatible)
- TCP binary (maximum performance)
- IPC (for testing purpose only)

BoutDuTunnel can also be directly hosted inside IIS or Apache/mod_mono (HTTPS protocols are then supported)

French and English translations are supplied.
