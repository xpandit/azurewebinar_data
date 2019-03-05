using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace Webinar.Data.AzureTable.Model
{
    public abstract class AzureTableEntity : TableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public AzureTableEntity()
        {
            this.RowKey = Id.ToString();
        }
    }
}