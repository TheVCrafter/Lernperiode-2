# Lern-Periode 2

25.10 bis 20.12

## Grob-Planung
Um mich in dieser Lernperiode genügend zu **fordern**, und auch viel neues über C# zu lernen, habe ich mir vorgenommen, ein Pac-Man in der Konsole zu programmieren. Das Pac-Man soll grundsätzlich wie das originale Spiel funktionieren. Also unteranderem über verschiedene Levels (zwischen 5-10), einen Highscore, verschiedene Menüs (Hauptmenü und Pausenmenü etc.) und auserdem über die originalen Sounds des Spieles verfügen. Das Ziel soll es wie beim Original sein, alle Punkte in einem Level aufzufressen, ohne von den verschiedenen Geistern getötet zu werden. Der Spieler soll ausserdem 3 Leben haben.

## 25.10.2024

- [x] Pac-Man design und Animation entwerfen
- [x] Experimentieren, wie man den Pac-Man in der Konsole steuern kann

Heute habe ich es geschafft, die Steuerung vom PacMan zu Programmieren. Obwohl ich anfänglich Schwierigkeiten hatte, klappte dies, nachdem ich im Internet herausgefunden hatte, wie man einlesen kann welche Taste gedrückt wird, relativ gut. Jedoch sieht es im Moment noch ein Wenig abgehackt aus. Für den PacMan und seine verschiedenen Phasen, habe ich die Zeichen □, ⊏, ⊐, ⊓ und ⊔ verwendet. Da es sich dabei jedoch um Mathematische Zeichen handelt, wurden diese zuerst als "?" angezeigt. Um diesen Fehler zu beheben musste ich deshalb `Console.OutputEncoding = System.Text.Encoding.UTF8;` anwenden.

## 1.11.2024

- [ ] Herausfinden, wie der Pac-Man die Punkte aufsammeln kann
- [ ] Score und Highscore anzeigen und den Highscore speichern
- [ ] Die Geister dazu bringen, den Spieler zu verfolgen und zu töten
- [ ] Die Leben des Spielers auf 3 setzen und beim Tod um ein Leben runtersetzen



## ...

