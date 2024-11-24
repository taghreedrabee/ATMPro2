using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;

namespace ATMapp
{
    internal class PendingTransfer
    {

        [JsonPropertyName("transferId")]
        public Guid TransferId { get; set; }

        [JsonPropertyName("senderId")]
        public Guid SenderId { get; set; }

        [JsonPropertyName("senderUsername")]
        public string SenderUsername { get; set; }

        [JsonPropertyName("recipientId")]
        public Guid RecipientId { get; set; }

        [JsonPropertyName("amount")]
        public double Amount { get; set; }

        [JsonPropertyName("dateTime")]
        public DateTime DateTime { get; set; }

        public PendingTransfer() { } 

        public PendingTransfer(Guid transferId, Guid senderId, string senderUsername, Guid recipientId, double amount)
        {
            TransferId = transferId;
            SenderId = senderId;
            SenderUsername = senderUsername;
            RecipientId = recipientId;
            Amount = amount;
            DateTime = DateTime.Now;
        }
        private const string PendingTransfersFilePath = "pending_transfers.json";
        private readonly object _fileLock = new object();

        
        private List<PendingTransfer> LoadPendingTransfers()
        {
            lock (_fileLock)
            {
                try
                {
                    
                    if (!File.Exists(PendingTransfersFilePath))
                    {
                        File.WriteAllText(PendingTransfersFilePath, "[]");
                        return new List<PendingTransfer>();
                    }

                    
                    string json = File.ReadAllText(PendingTransfersFilePath);
                    return JsonSerializer.Deserialize<List<PendingTransfer>>(json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                        ?? new List<PendingTransfer>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error loading pending transfers: {ex.Message}");
                    return new List<PendingTransfer>();
                }
            }
        }

        private void SavePendingTransfers(List<PendingTransfer> pendingTransfers)
        {
            lock (_fileLock)
            {
                try
                {
                    var options = new JsonSerializerOptions
                    {
                        WriteIndented = true, 
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };

                    string json = JsonSerializer.Serialize(pendingTransfers, options);
                    File.WriteAllText(PendingTransfersFilePath, json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving pending transfers: {ex.Message}");
                }
            }
        }

        public void AddPendingTransfer(PendingTransfer pendingTransfer)
        {
            var pendingTransfers = LoadPendingTransfers();
            pendingTransfers.Add(pendingTransfer);
            SavePendingTransfers(pendingTransfers);
        }

        public IEnumerable<PendingTransfer> GetPendingTransfersBySenderId(Guid senderId)
        {
            var pendingTransfers = LoadPendingTransfers();
            return pendingTransfers.Where(t => t.SenderId == senderId).ToList();
        }

        public IEnumerable<PendingTransfer> GetPendingTransfersByRecipientId(Guid recipientId)
        {
            var pendingTransfers = LoadPendingTransfers();
            return pendingTransfers.Where(t => t.RecipientId == recipientId).ToList();
        }

        public void DeletePendingTransfer(Guid transferId)
        {
            var pendingTransfers = LoadPendingTransfers();
            var pendingTransfer = pendingTransfers.FirstOrDefault(t => t.TransferId == transferId);

            if (pendingTransfer != null)
            {
                pendingTransfers.Remove(pendingTransfer);
                SavePendingTransfers(pendingTransfers);
            }
        }

        
        public bool PendingTransferExists(Guid transferId)
        {
            var pendingTransfers = LoadPendingTransfers();
            return pendingTransfers.Any(t => t.TransferId == transferId);
        }

       
        public void CleanupOldPendingTransfers(int daysOld = 30)
        {
            var pendingTransfers = LoadPendingTransfers();
            var cutoffDate = DateTime.Now.AddDays(-daysOld);

            pendingTransfers.RemoveAll(t => t.DateTime < cutoffDate);
            SavePendingTransfers(pendingTransfers);
        }
    }
}



    

