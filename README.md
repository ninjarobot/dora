# dora
DHCP protocol service
========

Discover -> Offer -> Request -> Acknowledge

### Discover
A client will broadcast a DHCPDISCOVER message (1) on a network to find a DHCP server.

### Offer
A server will broadcast a DHCPOFFER message (2) on a network to offer up an IP address.

### Request
A client will broadcast a DHCPREQUEST message (3) on a network to request to offered IP address be assigned to it.

### Acknowledge
A server will broadcast a DHCPACK message (6) to confirm the client is assigned that address.

This provides an implementation of a DHCP server for providing IP address and related service addresses to systems based on application data.
