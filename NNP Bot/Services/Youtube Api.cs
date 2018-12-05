using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Discord.Addons.Interactive;
using System;
using System.Collections.Generic;
using Discord;
using Discord.Commands;
using SearchResult = Google.Apis.YouTube.v3.Data.SearchResult;

namespace NNP_Bot.Services
{
    class Youtube_Api
    {
        private string ytApiKey = "AIzaSyCBXGF_M-o0HYYPYTh8LZWCPGAZfp97j4o";
        public async Task GetYtResults(SocketCommandContext context, string query)
        {
            var YoutubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = ytApiKey,
                ApplicationName = "SiroBot"
            });
            var SearchListRequest = YoutubeService.Search.List("snippet");
            var search = System.Net.WebUtility.UrlEncode(query);
            SearchListRequest.Q = search;
            SearchListRequest.MaxResults = 10;

            var searchListResponse = await SearchListRequest.ExecuteAsync();

            List<string> videos = new List<string>();
            List<string> channels = new List<string>();
            List<string> Playlist = new List<string>();

            foreach (var searchResult in searchListResponse.Items)
            {
                switch ((searchResult.Id.Kind))
                {
                    case "youtube#video":
                        {
                            videos.Add(string.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.VideoId));
                            break;
                        }

                    case "youtube#channel":
                        {
                            channels.Add(string.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                            break;
                        }

                    case "youtube#playlist":
                        {
                            Playlist.Add(string.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                            break;
                        }
                }

                await context.Channel.SendMessageAsync(string.Format("Videos: " + Constants.vbLf + "{0}" + Constants.vbLf, string.Join(@"\n", videos)));
                await context.Channel.SendMessageAsync(string.Format("Channels: " + Constants.vbLf + "{0}", string.Join(@"\n", channels)));
                await context.Channel.SendMessageAsync(string.Format("Playlists: " + Constants.vbLf + "{0}", string.Join(@"\n", Playlist)));
            }
        }
        public async Task<string> GetYtURLAsync(SocketCommandContext Context, string name, InteractiveService interactive, Discord.Rest.RestUserMessage msg)
        {
            // this.GetType().ToString()
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCBXGF_M-o0HYYPYTh8LZWCPGAZfp97j4o",
                ApplicationName = "SoraBot"
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            var search = System.Net.WebUtility.UrlEncode(name);
            searchListRequest.Q = search;
            searchListRequest.MaxResults = 10;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            List<string> videos = new List<string>();
            List<Google.Apis.YouTube.v3.Data.SearchResult> videosR = new List<Google.Apis.YouTube.v3.Data.SearchResult>();

            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        {
                            videos.Add(String.Format("{0}", searchResult.Snippet.Title));
                            videosR.Add(searchResult);
                            break;
                        }
                }
            }
            int indexx = 0;
            if (videos.Count > 1)
            {
                var eb = new EmbedBuilder()
                {
                    Color = new Color(4, 97, 247),
                    Title = "Enter the Index of the YT video you want to add."
                };
                string vids = "";
                int count = 1;
                foreach (var v in videos)
                {
                    vids += "**{count}.** {v}" + Constants.vbLf;
                    count += 1;
                }
                eb.Description = vids;
                var del = await Context.Channel.SendMessageAsync("", embed: eb);
                await del.DeleteAsync();
                if (indexx > (videos.Count) || indexx < 1)
                {
                    await msg.ModifyAsync(x =>
                    {
                        x.Content = ":no_entry_sign: Invalid Number";
                    });
                    return "f2";
                }
            }
            else
                indexx = 1;
            // await Context.Channel.SendMessageAsync(String.Format("Videos: \n{0}\n", String.Join("\n", videos)));

            return "https://www.youtube.com/watch?v={videosR[index-1].Id.VideoId}";
        }

        public static List<string> GetTitle(string name)
        {
            List<string> info = new List<string>();
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCBXGF_M-o0HYYPYTh8LZWCPGAZfp97j4o",
                ApplicationName = "SoraBot"
            });
            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = name;
            searchListRequest.MaxResults = 10;
            var searchListResponse = searchListRequest.Execute();
            foreach (SearchResult searchResult in searchListResponse.Items)
            {
                if (searchResult.Id.Kind == "youtube#video")
                {
                    info.Add(searchResult.Snippet.Title);
                    info.Add(searchResult.Id.VideoId);
                    break;
                }
            }
            return info;
        }
    }
}
