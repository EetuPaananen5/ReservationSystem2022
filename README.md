# ReservationSystem2022
Kyseesä backend toteutus jossa käytetään kolmikerros arkkitehtuuria. Controlleri on uloin kerros joka ottaa vasta ja lähettää esim http viestejä, Controller on yhteydessä serviceen jossa tehdään muutoksia ja repository luokat tallentavat tiedot tietokantaan.
Varaus järjestelmään on koodattu item luokat ja lopuksi vielä reservation luokat. Myös image ja user luokat toimivat. SQL tietokantaan on tallenntettu erilaista dataa postmanin avulla jolla pystyi tarkistamaan toimiiiko koodit oikein. <br>

Käyttäjän pystyy luomaan ja tehdä muutoksia ja poistaa,
Varattavia kohteita voi luoda, muokata ja poistaa
Kohteita ei voi poistaa jos ne on varattu
<br>

Kohteen varaamisesta tarkistetaan onko samalle kohteelle jo päällekäisiä varauksia
<br>
Omia varauksia pystyy etsimään luomaan ja muokkaamaan
