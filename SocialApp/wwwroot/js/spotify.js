export function createSpotifyPlayer(accessToken, deviceName) {
    return new Spotify.Player({
        name: deviceName,
        getOAuthToken: cb => { cb(accessToken); },
        volume: 1
    });
}

export function setSpotifyPlayerListeners(spotifyPlayer) {
     spotifyPlayer.addListener('ready', ({ device_id }) => {
        DotNet.invokeMethodAsync('SocialApp', 'SetSpotifyDeviceId', device_id);
    });

    spotifyPlayer.addListener('not_ready', ({ device_id }) => {
        console.log('Você saiu do Spotify', device_id);
    });

    spotifyPlayer.addListener('initialization_error', ({ message }) => {
        console.error(message);
    });

    spotifyPlayer.addListener('authentication_error', ({ message }) => {
        DotNet.invokeMethodAsync('SocialApp', 'RefreshSpotifyToken');
        console.warn(message);
    });

    spotifyPlayer.addListener('account_error', ({ message }) => {
        debugger
        console.error(message);
    });

    spotifyPlayer.addListener('player_state_changed', (state => {
        if (!state) {
            return;
        }

        DotNet.invokeMethodAsync('SocialApp', 'SpotifyStateHasChanged', state)
            .then(success => {
                if (!success) {
                    console.warn("Falha ao atualizar status do Spotify!");
                }
            });
    }));
}

export async function connectSpotifyPlayer(spotifyPlayer) {
    spotifyPlayer.connect().then(success => {
        if (success) {
            console.log('Você está conectado ao Spotify!');
        }
    });
}

export async function disconnectSpotifyPlayer(spotifyPlayer) {
    spotifyPlayer.disconnect().then(success => {
        if (success) {
            console.log('Você desconectou do player do Spotify.');
        }
    });
}

export async function play(spotifyPlayer, deviceName) {
    DotNet.invokeMethodAsync('SocialApp', 'TransferPlaybackToPlayerJS', deviceName).then(shouldPlay => {
        if (shouldPlay) {
            spotifyPlayer.resume();
        } else {
            spotifyPlayer.pause();
        }
    });
}