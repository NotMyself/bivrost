# Bivrost

![Bivrost](/docs/images/bivrost-readme-header.jpg?raw=true "Bivrost")

"In Norse mythology, Bivrost is a burning rainbow bridge that reaches between Midguard and Asgard, the realm of the gods." - [WikiPedia](https://en.wikipedia.org/wiki/Bifr%C3%B6st)

## About

Bivrost is a asp.net core application that offers several overlay endpoints, hosts a Twitch.tv bot and bridges the two together to display chat events in OBS while streaming.

## Getting Started

### Running Locally

As long as you have the development dependencies installed, the application can be run on your bare metal machine.

1. Clone the repository: `git clone https://github.com/NotMyself/bivrost.git`.
1. Change directory into the cloned repository `cd bivrost`.
1. Run script `scripts/local-init`.
1. Run the command `dotnet user-secrets set BIVROST_TWITCH_BOT_USER_NAME {bot-user-name}`.
1. Run the command `dotnet user-secrets set BIVROST_TWITCH_BOT_ACCESS_TOKEN {bot-access-token}`.
1. Run the command `dotnet user-secrets set BIVROST_TWITCH_BOT_CHANNEL {twitch-channel}`.
1. Run the command `dotnet user-secrets set BIVROST_TWITCH_CLIENT_ID {twitch-client-id}`.
1. Run the command `dotnet user-secrets set BIVROST_TWITCH_CLIENT_SECRET {twitch-client-secret}`.
   - **Note:** You must supply valid values for the **BIVROST_** environment variables, see [this](#obtaining-twitch-access-tokens) for instructions on how to obtain these values.
1. Run script `scripts/local-start`.

### Running in Docker

The application can easily be executed locally with no development dependencies using [Docker](https://www.docker.com/).

1. Clone the repository: `git clone https://github.com/NotMyself/bivrost.git`.
2. Change directory into the cloned repository `cd bivrost`.
3. Run script `scripts/image-create`.
4. Run script `scripts/image-start`.

### Running on Zeit Now

If you want to start using Bivrost as is with no customizations, this can easily be done with Zeit's Now platform.

1. Create a Zeit Now [account](https://zeit.co/signup).
2. Add environment secrets to your Now account for each [environment variable listed](#obtaining-twitch-access-tokens).
   - **Note:** This currently can only be done using the [Now CLI](https://zeit.co/download#now-cli).
   - Now CLI Command, ex: `now secrets add BIVROST_TWITCH_BOT_CHANNEL iamnotmyself`
3. Browes to the repository on Github: [Bivrost](https://github.com/NotMyself/bivrost)
4. Click the **Fork** button located on the right of the screen just under the main menu.
5. Select the GitHub account you want to fork the repository too.
6. Edit the **now.json** file and remove the **alias** property.
   - **Note:** This tells my deployment to automatically assign this alias on successful deployments. You can set up something similar, [details](https://zeit.co/docs/v2/domains-and-aliases/aliasing-a-deployment).
7. Connect the [Now for GitHub](https://zeit.co/github) application to your account.

Once complete, Now will deploy an instance of Bivrost on every clean commit to your master branch. By default, it will also do a test deployment for app pull requests submitted.

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

Bivrost also makes call directly to the Twitch APIs. This requires to you to register the application to get client crendentials.

1. Navigate to your [Twitch Developer Console](https://dev.twitch.tv/console).
1. Click the **Register Your Application** button.
1. Add **Bivrost** to the **Name** field.
1. Add **http://example.com** to the **OAuth Redirect URL** field.
   - **Note:** Bivrost does not currently use oAuth based authentication, so this value is not used.
1. Select **Application Integration** from the **Category** dropdown.
1. Click the **Create** button.

| Env Var | Note |
|---|---|
| BIVROST_TWITCH_BOT_USER_NAME | Twitch Account Username for Bot |
| BIVROST_TWITCH_BOT_ACCESS_TOKEN | OAuth Access Token for Twitch Account |
| BIVROST_TWITCH_BOT_CHANNEL | Twitch Chat Channel for Bot to join and listen. |
| BIVROST_TWITCH_CLIENT_ID | Twitch Application Client Id |
| BIVROST_TWITCH_CLIENT_SECRET| Twitch Application Secret |

### Screen Shots

#### Bivrost On Join Message

When the bot is successfully configured, it will announce it's arrival in chat like this:

![Bivrost On Join Message](/docs/images/bivrost-on-join-messsage.png?raw=true "Bivrost On Join Message")

#### Bivrost On Message Overlay

![Bivrost On Message Overlay](/docs/images/bivrost-on-message-overlay.gif?raw=true "Bivrost On Message Overlay")


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
