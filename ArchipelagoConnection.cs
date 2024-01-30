﻿using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Packets;
using Atto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReventureEndingRando
{
    class ArchipelagoConnection
    {
        public static ArchipelagoSession session;
        public static int requiredEndings;
        private string slot;
        private string server;

        public ArchipelagoConnection(string host, string slot)
        {
            string[] hostSplit = host.Split(':');
            session = ArchipelagoSessionFactory.CreateSession(hostSplit[0], int.Parse(hostSplit[1]));
            this.slot = slot;
            this.server = host;
        }

        public void Connect()
        {
            LoginResult result;

            try
            {
                result = session.TryConnectAndLogin("Reventure", slot, ItemsHandlingFlags.AllItems);
            } catch (Exception e)
            {
                result = new LoginFailure(e.GetBaseException().Message);
            }

            if (!result.Successful)
            {
                LoginFailure failure = (LoginFailure)result;
                string errorMessage = $"Failed to Connect to {server} as {slot}:";
                foreach (string error in failure.Errors)
                {
                    errorMessage += $"\n    {error}";
                }
                foreach (ConnectionRefusedError error in failure.ErrorCodes)
                {
                    errorMessage += $"\n    {error}";
                }

                Plugin.PatchLogger.LogInfo(errorMessage);
                return; // Did not connect, show the user the contents of `errorMessage`
            }

            var slotData = session.DataStorage.GetSlotData(ArchipelagoConnection.session.ConnectionInfo.Slot);
            requiredEndings = int.Parse(slotData["endings"].ToString());

            // Successfully connected, `ArchipelagoSession` (assume statically defined as `session` from now on) can now be used to interact with the server and the returned `LoginSuccessful` contains some useful information about the initial connection (e.g. a copy of the slot data as `loginSuccess.SlotData`)
            var loginSuccess = (LoginSuccessful)result;
        }

        public static async void Check_Send_completion()
        {
            var statusUpdatePacket = new StatusUpdatePacket();
            statusUpdatePacket.Status = ArchipelagoClientState.ClientGoal;
            await session.Socket.SendPacketAsync(statusUpdatePacket);
        }
    }
}
