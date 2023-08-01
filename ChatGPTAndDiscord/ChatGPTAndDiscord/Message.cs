using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OpenAI.GPT3.Extensions;
using OpenAI.GPT3.Interfaces;
using OpenAI.GPT3.ObjectModels;
using OpenAI.GPT3.ObjectModels.RequestModels;
using OpenAI.GPT3.ObjectModels.ResponseModels;

namespace ChatGPTAndDiscord
{
    public class Messages : ModuleBase
    {
        /// <summary>
        /// [ch]というコメントが来た際の処理
        /// </summary>
        /// <returns>Botのコメント</returns>
        [Command("ch", RunMode = Discord.Commands.RunMode.Async), Alias("text")]
        public async Task rl(string text)
        {
            var apiKey = ""; // ここにAPIキーを入力
            var apiEndpoint = "https://api.openai.com/v1/engines/davinci-codex/completions"; // ここにAPIエンドポイントを入力
            var prompt = text; // プロンプトの例
            var maxTokens = 120; // 応答の最大トークン数
            var temperature = 0.5; // 応答の生成に使用する温度

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestBody = new
                {
                    prompt = prompt,
                    max_tokens = maxTokens,
                    temperature = temperature
                };

                var httpContent = new StringContent(JsonConvert.SerializeObject(requestBody));
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                using (var response = await httpClient.PostAsync(apiEndpoint, httpContent))
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                    // JSON文字列を辞書に変換
                    Dictionary<string, object> dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent);
                    dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(dictionary["choices"].ToString());
                    await ReplyAsync(dictionary["text"].ToString());
                }
            }
        }
    }
}
