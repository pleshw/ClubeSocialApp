const TAB_ID = crypto.randomUUID(); // Generate a unique ID for the current tab
const PLAYER_KEY = "spotifyFirstTab"; // Key to store the first tab info in localStorage
let isFirstTab = false; // Tracks if this tab is the first to start the player

const channel = new BroadcastChannel("spotifyPlayerChannel");

function setFirstTab() {
    const currentOwner = localStorage.getItem(PLAYER_KEY);
    if (!currentOwner) {
        localStorage.setItem(PLAYER_KEY, TAB_ID);
        isFirstTab = true;
        channel.postMessage({ tabId: TAB_ID, action: "claimed" });
    }
}

channel.onmessage = (message) => {
    if (message.data.action === "claimed" && message.data.tabId !== TAB_ID) {
        isFirstTab = false;
    }
};

window.addEventListener("beforeunload", () => {
    if (isFirstTab) {
        channel.postMessage({ tabId: TAB_ID, action: "released" });
    }
});