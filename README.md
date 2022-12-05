# TextRPG
A text-based role-playing adventure game developed to showcase object-oriented/game design patterns. Created for CPSC 3175: Object-Oriented Design.

## About
A simple text-based, command-line driven RPG built in C#. Incorporates various game design patterns.
This is still a work in progress as I update my old code so elements are likely to change and features may not work correctly.

## Demonstration
Game starts with a "Title Screen" and asks the user for a username.
![Title Screen](screenshots/titleView.png?raw=true)

User is then placed into an area and their surroundings are displayed.
![Area Screen](screenshots/areaView.png?raw=true)

If player types 'help', they are shown available commands and their information.
![Help Screen](screenshots/helpView.png?raw=true)

If player types 'inventory', they are shown the contents of their inventory.
![Inventory Screen](screenshots/inventoryView.png?raw=true)

Player can also type 'move [direction]' to move to another area.
![Move Screen](screenshots/moveView.png?raw=true)

If current area features a merchant character, the player can type 'trade' to interact.
![Trade Screen](screenshots/tradingView.png?raw=true)

Player can use buy and sell commands to trade with the merchant.
Updated inventory is shown along with remaining storage capacity and funds left.
![Buy Screen](screenshots/tradingView2.png?raw=true)

Player can also encounter an enemy.
![Fighting Screen](screenshots/fightingView.png?raw=true)

Player can use weapons against an enemy. Enemy will fight back.
Updated healthbars are shown as fight progresses.
![Healthbar Screen](screenshots/fightingView1.png?raw=true)

Player gains experience and coins from defeating the enemy.
The experience can be used to upgrade player stats, like health, strength, and carrying capacity.
![Victor Screen](screenshots/fightingView2.png?raw=true)
