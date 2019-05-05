# Bivrost

![Bivrost](/docs/images/bivrost_header.jpg?raw=true "Bivrost")

"In Norse mythology, Bivrost is a burning rainbow bridge that reaches between Midguard and Asgard, the realm of the gods." - [WikiPedia](https://en.wikipedia.org/wiki/Bifr%C3%B6st)

## About

## Getting Started

### Running in Docker

1. Clone the repository: `git clone https://github.com/NotMyself/bivrost.git`.
2. Change directory into the cloned repository `cd bivrost`.
3. Run script `scripts/image-create`.
4. Run script `BIVROST_TWITCH_BOT_USER_NAME={bot-user-name} BIVROST_TWITCH_BOT_ACCESS_TOKEN={bot-access-token} BIVROST_TWITCH_BOT_CHANNEL={twitch-channel} scripts/image-start`.
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

Bivrost uses several bits of information to connect to stream chat.

In most cases, you can use your Twitch username for both the bot username and channel. This will make the bot appear as you in chat.

If you would like to have a seperate bot username from your own, you will need to create that account on Twitch and use it to generate your access token.

You will need to generate an OAuth authorization token with several specific scopes applied to it.

1. Navigate to [Twitch Token Generator](https://twitchtokengenerator.com).
2. Select the **Custom Scope Token** option in the popup dialog.
3. Ensure the following scopes are enabled:
   1. `user_read`
   2. `user_blocks_edit`
   3. **TODO:** Figure out the exact scopes needed, for now select all.
4. Click the **Generate Token!** button.
5. You will be redirected Twitch, authenticate if needed.
6. Click the **Authorize** button to grant Twitch Token Generator concent to access your account with the listed scopes.
7. Verify you are not a robot.
8. Click the **Copy** button next to **Access Token**.


| Env Var | Note |
|---|---|
| BIVROST_TWITCH_BOT_USER_NAME | Twitch Account Username for Bot |
| BIVROST_TWITCH_BOT_ACCESS_TOKEN | OAuth Access Token for Twitch Account |
| BIVROST_TWITCH_BOT_CHANNEL | Twitch Chat Channel for Bot to join and listen. |

### External Dependencies

#### .NET Core

#### Node 8

### Docker

### Platform Dependencies

- [ASP.NET Core](https://www.asp.net/mvc)
- [Vue.js](https://vuejs.org/)

### Features

- [Chat Bot](docs/mockups/chat-bot/readme.md)

### Architecture

- [Logging](docs/logging.md)
