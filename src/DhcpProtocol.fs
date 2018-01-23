namespace Dora

module DhcpProtocol =
    
    type Octet = byte
    type Octet2 = Octet seq
    type Octet4 = Octet seq
    type Octet16 = Octet seq
    type Octet64 = Octet seq
    type Octet128 = Octet seq

    type MessageField =
        /// Message op code / message type.
        /// 1 = BOOTREQUEST, 2 = BOOTREPLY
        | Op of Octet
        /// Hardware address type, see ARP section in 
        // "Assigned Numbers" RFC; e.g., '1' = 10mb ethernet.
        | Htype of Octet
        /// Hardware address length (e.g.  '6' for 10mb ethernet).
        | Hlen of Octet
        /// Client sets to zero, optionally used by relay agents
        /// when booting via a relay agent.
        | Hops of Octet
        /// Transaction ID, a random number chosen by the client, 
        /// used by the client and server to associate messages 
        /// and responses between a client and a server.
        | Xid of Octet4
        /// Filled in by client, seconds elapsed since client
        /// began address acquisition or renewal process.
        | Secs of Octet2
        /// Flags
        | Flags of Octet2
        /// Client IP address; only filled in if client is in
        /// BOUND, RENEW or REBINDING state and can respond
        /// to ARP requests.
        | Ciaddr of Octet4
        /// 'your' (client) IP address.
        | Yiaddr of Octet4
        /// IP address of next server to use in bootstrap;
        /// returned in DHCPOFFER, DHCPACK by server.
        | Siaddr of Octet4
        /// Relay agent IP address, used in booting via a
        /// relay agent.
        | Giaddr of Octet4
        /// Client hardware address.
        | Chaddr of Octet16
        /// Optional server host name, null terminated string.
        | Sname of Octet64
        /// Boot file name, null terminated string; "generic"
        /// name or null in DHCPDISCOVER, fully qualified
        /// directory-path name in DHCPOFFER.
        | File of Octet128
        /// Optional parameters field.
        | Options of Octet list

    let buildOctets (bytes:byte seq) (count:uint32) =
        let remaining = ref count
        seq {
            use e = bytes.GetEnumerator ()
            while !remaining > 0u do
                remaining := !remaining - 1u
                if e.MoveNext () then
                    yield e.Current
                else
                    yield 0uy
        }

    let buildOctet2 (bytes:byte seq) : Octet2 =
        buildOctets bytes 2u

    let buildOctet4 (bytes:byte seq) : Octet4 =
        buildOctets bytes 4u

    let buildOctet16 (bytes:byte seq) : Octet16 =
        buildOctets bytes 16u

    let buildOctet64 (bytes:byte seq) : Octet64 =
        buildOctets bytes 64u

    let buildOctet128 (bytes:byte seq) : Octet128 =
        buildOctets bytes 128u

    let buildSname (serverName:string) =
        // Get first 63 bytes, as a null must be added.
        serverName
        |> System.Text.Encoding.ASCII.GetBytes
        |> Seq.truncate 63
        |> buildOctet64
    