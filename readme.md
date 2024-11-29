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

- [X] Pathfinding des Geistes so verändern dass er den Pac Man verfolgt, wenn dieser in Sichtweite ist
- [X] Implementation der Kraftpille abschliessen
- [X] Fehler bei der "Berührungserkennung" beheben
- [X] Weitere Menüs und Anzeigen ins Spiel einfügen

Zu Beginn habe ich mich heute damit befasst, die Verfolgungsmechanik des Geistes zu programmieren. Ich brachte den Geist schliesslich dazu, in die Richtung des PacMans zu laufen, wenn dieser sich auf der gleichen Y-Position befand und sich keine Mauer zwischen den beiden Figuren befand. Jedoch funktionierte das Gleiche aus einem mir unbekannten Grund nicht für die gleiche X-Position. Ich versuchte auf verschiedenste Arten den Fehler zu beheben und prüfte unteranderem ob ich eventuell die Richtungen vertauscht hatte. Als dies alles dann aber nichts nützte begann ich mit dem Programmieren der Kraftpille und beschloss, die Verfolgungsmechanik des Geistes in der nächsten Woche zu beenden. Bei der Kraftpille verwendete ich anschliessend einen ähnlichen Code, wie ich ihn schon zum auslesen der gesammelten Punkte verwendet hatte. Anschliessend erstellte ich einen Int um die Wirkungsdauer festzulegen. Ich gab den Int einen Wert von 20 (da ein Tick in meinem Spiel 0.15 Sekunden lang ist beträgt die Wirkungsdauer also 3 Sekunden). Ich werde die Wirkungsdauer aber in Zukunft eventuell noch verlängern. Anschliessend stellte ich sicher, dass bei aktiver Kraftpille nicht der PacMan vom Geist, sondern der Geist vom PacMan gefressen wird und das die Punktezahl erhöht wird. Ich legte für das Töten des Geistes eine Punktezahl von 15 fest. Zum Schluss schaute ich auch, dass der Geist wie im echten Spiel blau wird wenn man eine Kraftpille aufsammelt. Dann versuchte ich noch mein Spiel etwas schöner zu gestalten und erstellte neben dem Bildschirm bei einer Niederlage, welchen ich ja bereits letztes mal erstellt hatte, noch einen Bildschirm bei einem Sieg und ausserdem noch ein Hauptmenü. Das ganze versuchte ich anschliessend noch mit Ascii-Art zu verschönern. Als ich damit fertig war, legte ich die Punktezahl, welche für einen Sieg benötigt wird auf 150 fest. Da in der Map aber nur 135 Punkte vorhanden sind muss man also mindestens einmal den Geist töten um zu gewinnen. Als letztes Programmierte ich noch eine Pausier-Funktion. Es ist nun also möglich das Spiel mit drücken der Escape-Taste zu pausieren und anschliessend wieder fortzusetzen.

## 29.11.2024

- [ ] Verfolgungsmechanismus des Geistes verbessern
- [X] Design der Menüs eventuell anpassen
- [X] Sound ins Spiel einfügen

Heute habe ich als erstes den Bildschirm welcher nach einer Niederlage erscheint und den Bildschirm welcher nach einem Sieg erscheint etwas angepasst indem ich jeweils ein Besseres Ascii-Art eingefügt habe. Anschliessend suchte ich lange nach Sounds, welche ich in das Spiel einbauen könnte. Ich stiess relativ schnell auf den Start-Sound und die Titelmusik des Spiels, hatte jedoch grosse Mühe die weiteren Sounds zu finden. Nachdem ich weiter recherchiert hatte, fand ich dann schliesslich bei einem Reddit-Post einen Link zu einer Website, auf welcher alle Sounds des Spiels als .wav-Datei heruntergeladen werden konnten. Zunächst baute ich die Titelmusik und den Startsound ins Spiel ein. Dabei hatte ich aufgrund meiner in der letzten Lernperiode gesammelten Erfahrung mit der SoundPlayer-Methode glücklicherweise keine Probleme. Anschliessend fügte ich den Sirenen-Sound ein, welcher während dem Spiel abgespielt wird und sorgte mit `.PlayLooping();` dafür, dass dieser Sound nach komplettem Abspielen neu gestartet wird. Als ich jedoch den Sound welcher beim Fressen von Punkten entsteht einfügen wollte, bekam ich ein Problem. Ich stellte fest, dass ich nicht zwei Sounds gleichzeitig abspielen konnte. Ich probierte deshalb verschiedene Threads für die Sounds zu erstellen, damit diese gleichzeitig ausgeführt werden konnten, hatte jedoch aufgrund mangelnder Erfahrung grosse Probleme und schaffte es leider weiterhin nicht, zwei Sounds gleichzeitig abzuspielen. Ich überlegte ein wenig, ob ich eventuell ingame nur den Sirenen-Sound laufen lassen sollte, da ich mein PacMan-Spiel jedoch möglich realistisch machen will, entschied ich mich dagegen. Damit es mir aber nächste Woche möglich sein wird, zwei Sounds gleichzeitig abzuspielen, habe ich mir vorgenommen, bis nächsten Freitag weiter im Internet zu recherchieren und mich falls nötig mit Multithreading auseinandersetzen.

## 6.11.2024

- [ ] Soundproblem beheben (evtl. mit Multithreading)
- [ ] Restliche Sounds ins Spiel einfügen
- [ ] Verfolgungsmechanismus des Geistes verändern, damit er vollständig funktioniert
- [ ] Code mithilfe von Funktionen übersichtlicher gestalten
