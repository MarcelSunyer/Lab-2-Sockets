# Room Manager for Multiplayer Game

## Project Description
This project implements a **Room Manager** designed for a multiplayer video game, allowing players to create and join games efficiently through a **server-client architecture**. It consists of two primary scenes: the **Server Scene ("Create Game")** and the **Client Scene ("Join Game")**, enabling seamless player connections and communication.

---

## Server Scene: "Create Game"
The Server Scene serves as the game host, where players can create new game sessions. Upon startup, it initializes a socket to listen for incoming client connections. When a client sends a message with their username, the server responds with a confirmation, including its name or ID for identification.

### Bonus Features
- **Waiting Room**: Players who connect are directed to a waiting room where they can see other participants.
- **Chat Functionality**: A chat system is implemented, allowing players to exchange messages while waiting, enhancing interaction and engagement.

---

## Client Scene: "Join Game"
The Client Scene allows players to join existing game sessions by entering the server's IP address. Once the IP is inputted, the client connects to the server using both **UDP** and **TCP** protocols. Upon a successful connection, the client sends a message containing the player’s name.

### Bonus Features
- **Waiting Room**: Similar to the server, players enter a waiting room upon connection, where they can view other connected players.
- **Chat System**: Players can communicate in the waiting room, fostering a social atmosphere before the game starts.

---

## Networking Protocols
The project supports both **UDP** and **TCP** implementations:

- **UDP**: Used for quick, real-time updates and chat messages, prioritizing speed over reliability.
- **TCP**: Ensures reliable message delivery and stable connections, crucial for maintaining game state.

---

## Future Enhancements
Potential extensions include:

- **Player Limit Management**: Setting maximum player limits for games.
- **Customizable Player Profiles**: Allowing players to personalize their profiles.
- **Game State Management**: Storing and managing game states between sessions.
- **Cross-Platform Compatibility**: Ensuring functionality across various platforms (PC, consoles, mobile).
- **Security Features**: Implementing measures to protect connections from threats.

---

This **Room Manager** project provides a robust foundation for multiplayer game connectivity, enhancing player experience through interactive features and flexible communication options.
