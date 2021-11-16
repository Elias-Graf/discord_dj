Play local audio streams via a discord bot.

# Installation

1. Clone the repository.
1. Create a discord bot (if you haven't already).
1. Copy the bots client token and either:
    - set the `DISCORD_CLIENT_TOKEN` environment variable
    - create the file `appsettings.json` in the root of the project and add `{ "DISCORD_CLIENT_TOKEN": <token> }` to it.
1. Run the app using `dotnet run`.

# Capture & stream audio from a source

1. Add the bot to the server you want to use it in.
1. Join a voice channel and execute the `join` command to let the bot come and join you.
1. Select an audio stream using the `setCaptureDevice` command (you will need to select the stream inside the console you ran `dotnet start` from).
1. Enter the `startPlayback` command.
1. Profit.