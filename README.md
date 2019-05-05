# Bivrost

![Bivrost](/docs/images/bivrost_header.jpg?raw=true "Bivrost")

## About

## Getting Started

### Running in Docker

1. Clone the repository: `git clone https://github.com/NotMyself/bivrost.git`.
1. Change directory into the cloned repository `cd bivrost`.
1. Run script `scripts/image-create`.
1. Run script `BIVROST_TWITCH_BOT_USER_NAME={bot-user-name} BIVROST_TWITCH_BOT_ACCESS_TOKEN={bot-access-token} BIVROST_TWITCH_BOT_CHANNEL={twitch-channel} scripts/image-start`.
   - **Note:** You must supply valid values for the **BIVROST_** environment variables, see [this](#obtaining-twitch-access-tokens) for instructions on how to obtain these values.

### Running Locally

1. Clone the repository: `git clone https://github.com/NotMyself/bivrost.git`.
1. Change directory into the cloned repository `cd bivrost`.
1. Run script `scripts/local-init`.
1. Run the command `dotnet user-secrets set BIVROST_TWITCH_BOT_USER_NAME {bot-user-name}`.
1. Run the command `dotnet user-secrets set BIVROST_TWITCH_BOT_ACCESS_TOKEN={bot-access-token}`.
1. Run the command `dotnet user-secrets set BIVROST_TWITCH_BOT_CHANNEL={twitch-channel}`.
  - **Note:** You must supply valid values for the **BIVROST_** environment variables, see [this](#obtaining-twitch-access-tokens) for instructions on how to obtain these values.
1. Run script `scripts/local-start`.

### Obtaining Twitch Access Tokens

### External Dependencies

#### .NET


#### JavaScript, HTML, CSS


### Platform Dependencies

- [ASP.NET Core](https://www.asp.net/mvc)

### Features

- [Chat Bot](docs/mockups/chat-bot/readme.md)

### Architecture

- [Logging](docs/logging.md)
