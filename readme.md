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

- [ ] Anzeigen wie viele Punkte gesammelt wurden
- [x] Geist hinzufügen und dafür einen Pathfinding-Mechanismus entwickeln
- [ ] 3 Leben für den Pac-Man und Tod bei berührung mit dem Geist
- [ ] Der Pac-Man soll beim Fressen der auch im originalen Spiel enthaltenen Kraftpille die Geister fressen können und dafür Punkte erhalten

Heute habe ich vorallem an dem Pathfinding des Geistes gearbeitet. Dazu musste ich sicherstellen, das der Geist sich automatisch durch die Map bewegt und wie der PacMan keine Wände der Map zerstört. Ausserdem musste ich schauen, dass die Punkte nicht durch den Geist sondern nur vom PacMan aufgefressen werden können. Dies funktionierte auch zu beginn. Als ich jedoch am Pathfinding des Geistes weiter arbeitete, funktionierte dies nicht mehr wie gewollt und die Punkte verschwanden wenn der Geist einmal darüber lief und tauchten wieder auf wenn er ein zweites Mal darüber lief. Ausserdem hatte ich über lange Zeit das Problem, dass der Geist plötzlich einfach stecken blieb. Dies konnte ich nur teilweise beheben. Grundsätzlich funktioniert mein Pathfinding, der Geist folgt jedoch noch nicht dem PacMan und wie schon erwähnt treten einige Bugs auf. Deshalb werde ich mir auf nächstes Mal Primär vornehmen mein Programm zu debuggen und den Pathfinding-Mechanismus zu beenden.

## 15.11.2024

- [x] Pathfinding des Geistes debuggen und abschliessen
- [x] Tod des PacMan bei Berührung mit dem Geist und anschliessend hinzufügen das der PacMan 2 mal wiederbelebt wird und das Spiel bei einem 3. Tod beendet wird (3 Leben)
- [ ] GUI des Spiels verbessern (Hauptmenü, Punkteanzahl, Lebensanzeige, Endmenü bei Sieg und Endmenü bei Niederlage, Pausenmenü etc.)
- [ ] Zusatzfunktionen wie Kraftpille und Bonus-Früchte hinzufügen

Heute habe ich es endlich geschafft, dass das Pathfinding des Geistes richtig funktioniert. Ich musste dazu jedoch die Textur des Geistes ändern und ausserdem die Map etwas umdesignen. Dann hatte ich jedoch das Problem, dass die Zeichen der Map nicht aktualisiert wurden, wenn der Pac Man einen Punkt aufsammelte und deshalb der Geist die Punkte wieder platzierte wenn er über die Entsprechende stelle lief. Um dies zu verhindern erstelle ich anschliessend eine Funktion mit dem Namen "changeMapSymbols". Dann Programmierte ich die Leben und den Tod vom PacMan. Ich programmierte, das der Pac Man stirbt, wenn die Positionen vom Geist und dem Pac Man identisch sind. Die Positionen beider Figuren werden anschliessend zurückgesetzt und ein Leben wird abgezogen. Wenn der PacMan keine leben mehr hat geht es ins Endmenü und der Spieler kann entscheiden, ob er eine weitere Runde spielen möchte oder nicht. Als nächste Programmierte ich die Punkteanzeige und versuchte das Spiel etwas schöner zu gestalten. Zum Schluss begann ich dann noch kurz mit dem Programmieren der Kraftpille, wurde aber noch nicht ganz fertig. Beim Testen des Spiels viel mir dann auf, dass der Pac Man nicht immer stirbt wenn er mit dem Geist in Berührung kommt. Ich habe die Vermutung, das dieses Problem auftritt, weil die beiden Figuren wenn sie in entgegengetzte Richtungen unterwegs sind so aneinander vorbeigehen, dass sie nicht gleichzeitig die gleiche Position haben. Ich werde mich also nächstes Mal sicher um dieses Problem kümmern. Auch habe ich noch nicht implementiert, dass der Geist den Pac Man verfolgt, wenn dieser in Sichtweite ist.

## 22.11.2024

- [ ] Pathfinding des Geistes so verändern dass er den Pac Man verfolgt, wenn dieser in Sichtweite ist
- [ ] Implementation der Kraftpille abschliessen
- [ ] Fehler bei der "Berührungserkennung" beheben
- [ ] Weitere Menüs und Anzeigen ins Spiel einfügen 
