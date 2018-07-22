# tournamentGrid
A small console application that helps to keep track of the leaders in the game and the next game sessions
# Install
* clone this project folder
```text
npm install
```
```text
npm start [number of players]
```
**All actions are performed in the terminal. Running into the IDE does not guarantee the functionality of the application**
# Args
```bash
--grid [olympic, FIFA]
// --grid olympic - standart olympic grid
// --grid FIFA - visualization of the grid at the World Cup 2018
--loser // If you want a grid with losers (only when --grid olympic)
```
# Examples
```text
npm start
France
Belgium
Croatia
England
```
```text
npm start 8
Uruguay
France
Brazil
Belgium
Russia
Croatia
Sweden
England
```
```text
npm start 16
Uruguay
Portugal
France
Argentina
Brazil
Mexico
Belgium
Japan
Spain
Russia
Croatia
Denmark
Sweden
Switzerland
Colombia
England
```

# Credits
(C) 2018 Vladislav Tupikin. MIT License
