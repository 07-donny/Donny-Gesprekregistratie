# Donny-Gesprekregistratie
Eind praktijk opdracht jaar 2

﻿MVC Practicum: Gespreksregistratie
Je bent gevraagd een gespreksregistratiesysteem te ontwikkelen voor de klantenservice van een supermarkt. In dit systeem kunnen medewerkers telefoongesprekken met klanten registreren, die vervolgens kunnen worden bewaard voor latere afhandeling of afgesloten en gearchiveerd.
Functionele requirements
1.	Het aanmaken van medewerkers. Dit komt neer op een registratiepagina waar een medewerkersaccount kan worden geregistreerd met een gebruikersnaam en wachtwoord. 

2.	Het aanmaken en beheren van gesprekken. De gegevens die je bewaart voor een gesprek zijn:
•	Naam (van de klant)
•	Datum (automatisch invullen)
•	Soort melding (informatieverzoek, suggestie of klacht, kiesbaar dmv. een dropdown)
•	Onderwerp
•	Inhoud (textarea!)
•	Medewerker (type IdentityUser)
•	Afgesloten (ja/nee)

3.	Bij een geregistreerd gesprek moet het mogelijk zijn om als medewerker reacties toe te voegen en uiteindelijk de casus te sluiten. Voor een reactie bewaar je:
•	Datum (automatisch invullen)
•	Inhoud (textarea!)
•	Medewerker (type IdentityUser)


Verdere requirements
•	Zorg voor validatie bij verplichte velden
•	Maak gebruik van bootstrap voor een goed uitziende lay-out
•	Sla de gegevens op in de database
•	Gebruik een standaard .NET Core 3.1 MVC project (met individual user authentication)
