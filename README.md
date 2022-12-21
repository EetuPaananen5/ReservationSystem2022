# ReservationSystem2022
Kyseesä backend toteutus jossa käytetään kolmikerros arkkitehtuuria. Controlleri on uloin kerros joka ottaa vasta ja lähettää esim http viestejä, Controller on yhteydessä serviceen jossa tehdään muutoksia ja repository luokat tallentavat tiedot tietokantaan.
Varaus järjestelmään on koodattu item luokat ja lopuksi vielä reservation luokat. Myös image ja user luokat toimivat. SQL tietokantaan on tallenntettu erilaista dataa postmanin avulla jolla pystyi tarkistamaan toimiiiko koodit oikein. <br>

Käyttäjän User pystyy luomaan ja tehdä muutoksia ja poistaa. Salasana täytyy luoda ja käyttää salttia sekä base 64 modattua salasanaa. Tietoturva siis kunnossa.
<br>
Varattavia kohteita Item voi luoda, muokata ja poistaa
Kohteita ei voi poistaa jos ne on varattu. Vain kohteen omistajalla oikeus muokata itemejä.
<br>

Kohteen varaamisesta tarkistetaan onko samalle kohteelle jo päällekäisiä varauksia.
<br>
Omia varauksia reservations pystyy etsimään luomaan ja muokkaamaan.

