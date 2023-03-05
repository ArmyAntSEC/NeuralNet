
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace NeuralNetAsp.DynamoDb
{
  public class DynamoClient
  {

    public static async Task CreateDynamoClient()
    {
      var _amazonDynamoDBClient = new AmazonDynamoDBClient(
        new AmazonDynamoDBConfig
        {
          UseHttp = true,
        });

      Table table = Table.LoadTable(_amazonDynamoDBClient, "NeuralNetCdkStack-WeightParameterStorageCBDC95F8-183I7RJHSPN6H");

      var book = new Document();
      book["PK"] = "PK:" + DateTime.Now;
      book["SK"] = "SK:" + DateTime.Now;
      book["title"] = "Hello World";

      var task = await table.PutItemAsync(book);
      return;
    }
  }
}