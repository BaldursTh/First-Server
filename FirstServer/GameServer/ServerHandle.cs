using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace GameServer
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromCLient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromCLient].tcp.socket.Client.RemoteEndPoint} has connected and is player {_fromCLient}: {_username}");
            if (_fromCLient != _clientIdCheck)
            {
                Console.WriteLine("Something went very wrong with the client ids and cancer");
            }

            Server.clients[_fromCLient].SendIntoGame(_username);
        }
        public static void PlayerMovement(int _fromClient, Packet _packet)
        {
            bool[] _inputs = new bool[_packet.ReadInt()];
            for (int i = 0; i < _inputs.Length; i++)
            {
                _inputs[i] = _packet.ReadBool();
            }
            Quaternion _rotation = _packet.ReadQuaternion();
            Server.clients[_fromClient].player.SetInputs(_inputs, _rotation);

        }
    }
}
