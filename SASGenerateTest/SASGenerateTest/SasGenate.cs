using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Azure.Identity;

namespace SASGenerateTest
{
    class SasGenate
    {
        public void Run()
        {

            string userAssignedClientId = "";
            var credential = new DefaultAzureCredential(new DefaultAzureCredentialOptions { ManagedIdentityClientId = userAssignedClientId });

            BlobContainerClient client = new BlobContainerClient(new Uri(""), credential);

            BlobSasBuilder blobSasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = client.Name,
                Resource = "c",
                StartsOn = DateTime.Now.AddMinutes(-15),
                ExpiresOn = DateTime.Now.AddHours(10)
            };

            blobSasBuilder.SetPermissions(BlobContainerSasPermissions.Read);

            Console.WriteLine(client.GenerateSasUri(blobSasBuilder));

        }
    }
}
