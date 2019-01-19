# Email Scheduler App

*Invoice
https://dolce.box.com/s/eheggfoid0lbulgsbbta132wm6824bgi

*Project Roll-out
https://dolce.box.com/s/aomizbc6l0npasxiqri19js6l6ihuenc

DevTeam,Space Call Summary - R Dolce Group 12/24/2018
Alex Skryabikov <alex@devteam.space>
Mon 12/24/2018 2:26 PM
To:	Martinique Dolce <marti@rdolcegroup.com>;
Hi Marti,

thanks for your time on the call, please find a short summary below.

The outlined structure of the application looks as follows:

an Azure based Virtual Machine with a custom .Net app in there
a scheduler on Windows Azure parsing data from Onerooftop if that's possible (you mentioned that Onerooftop does not have any API to download reservations). Probably it will email reservations to some specific inbox later on
an AirTable spreadsheet and API to get property owners
once an in hour the .Net app will read the data from reservations, compare  it with the property data from AirTable and email the property owners with the data from reservations.

to proceed with the estimate we will need

an example list of reservations as a CSV
a Postman collection to connect to a specific spreadsheet on AirTable, you mentioned you have it
a functional specification of the use cases you want to cover on the first stage of the project

Thank you and wish you a great holiday season,
Alex Sk | VP Account Management
DevTeam.Space, Data-driven Software Development Platform
Customer Success Story - Airbnb Competitor
alex@devteam.space
415 570 7043