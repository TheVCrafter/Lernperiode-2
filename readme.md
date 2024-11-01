# Lern-Periode 2

25.10 bis 20.12

## Grob-Planung
Um mich in dieser Lernperiode genügend zu **fordern**, und auch viel neues über C# zu lernen, habe ich mir vorgenommen, ein Pac-Man in der Konsole zu programmieren. Das Pac-Man soll grundsätzlich wie das originale Spiel funktionieren. Also unteranderem über verschiedene Levels (zwischen 5-10), einen Highscore, verschiedene Menüs (Hauptmenü und Pausenmenü etc.) und auserdem über die originalen Sounds des Spieles verfügen. Das Ziel soll es wie beim Original sein, alle Punkte in einem Level aufzufressen, ohne von den verschiedenen Geistern getötet zu werden. Der Spieler soll ausserdem 3 Leben haben.

## 25.10.2024

- [x] Pac-Man design und Animation entwerfen
- [x] Experimentieren, wie man den Pac-Man in der Konsole steuern kann

Heute habe ich es geschafft, die Steuerung vom PacMan zu Programmieren. Obwohl ich anfänglich Schwierigkeiten hatte, klappte dies, nachdem ich im Internet herausgefunden hatte, wie man einlesen kann welche Taste gedrückt wird, relativ gut. Jedoch sieht es im Moment noch ein Wenig abgehackt aus. Für den PacMan und seine verschiedenen Phasen, habe ich die Zeichen □, ⊏, ⊐, ⊓ und ⊔ verwendet. Da es sich dabei jedoch um Mathematische Zeichen handelt, wurden diese zuerst als "?" angezeigt. Um diesen Fehler zu beheben musste ich deshalb `Console.OutputEncoding = System.Text.Encoding.UTF8;` anwenden.

## 1.11.2024

- [X] Map designen
- [X] Verhindern, dass der Pac-Man die Map verlassen kann
- [X] Punkte, welche vom Pac-Man eingesammelt werden können einfügen
- [X] Herausfinden, wie der Pac-Man die Punkte aufsammeln kann

Zu Beginn designte ich heute eine Map für das Spiel. Dazu erstellte ich im Text-Editor mit verschiedenen Strich-Arten ein Ascii-Art und fügte dieses anschliessend als String in mein Programm ein.
Anschliessend hatte ich jedoch das Problem, das der Pac-Man nicht in der Map blieb, sondern durch die Wand lief und dabei die Map zerstörte. Ich musste also einen Weg finden, dies zu verhindern. 
Dazu ich erstellte ich zunächst eine Funktion namens isThereAWall. Dann spaltete ich den Map-String mithilfe von `.split(\'n')` in seine einzelnen Zeile und speicherte die auf eine Wand zu prüfende Zeile in einem Char-Array. Anschliessend prüfte ich, ob die einzelnen Zeichen in der betroffenen Zeile ein Teil der Map sind. Zunächst funktionierte es nicht und der Pac-Man bewegte sich gar nicht mehr und ich wusste auch nicht genau woran dies liegen könnte. Doch ich entdeckte bald, dass dies daran lag, dass ich beim Designen der Map nicht nur Leerschläge, sondern auch Tabs verwendet hatte. Ich bearbeitete die Map also nochmals und ersetzte alle Tabs. Danach funktionierte das ganze zum Glück und ich wollte Punkte in das Spiel einfügen. Ich musste die Map also erneut bearbeiten, und einfach an bestimmten stellen · oder • einfügen. Das aufsammeln der Punkte stellte glücklicherweise kein Problem dar, da die Punkte eh durch einen Leerschlag ersetzt werden, wenn der Pac-Man darüber läuft.

## 8.11.2024

-[ ] Anzeigen wie viele Punkte gesammelt wurden
-[ ] Einen Geist hinzufügen und dafür einen Pathfinding-Mechanismus entwickeln
-[ ] 

