# MedicinskaLaboratorija_projekat
## Tok procesa ide sledecim redom:
Pacijent dolazi u laboratoriju, laborant ga pita da li je vec bio u laboratoriji, ako jeste, po id-ju pacijenta (jmbg/lbo) ga nalazi, ako nije unosi detalje pacijenta. Pacijent prica koje analize zeli da odradi, laborant ukucava redne brojeve analiza i popunjava radni nalog. Kada pacijent zavrsi sa listom zahteva, laborant stampa racun na osnovu cena svake analize. Laborant stampa listu analiza, uzima uzorak pacijenta i smesta ga u biohemijski aparat. Nakon sto laboratorijski aparat odradi analize, laborant unosi rezultate analiza u racunar. Po dolasku pacijenta, stampa rezultate analiza i daje ih pacijentu.
## Funkcionalnost:
Kroz vise formi obezbedjena je osnovna neophodna manipulacija bazom podataka (Med_lab Dijagram.png, uvid u logicki model):
1. Registracija, izmena i brisanje podataka pacijenata
2. Registracija, izmena i brisanje podataka zaposlenih
3. Popunjavanje tabela racuni i rezultati podacima ukucanim u formu (id pacijenta, spisak analiza, rezultati, id biohemicara potpisnika), uz neophodnu proveru postojanosti odredjenih podataka. Uz navedeno, realizovana je opcija stampanja racuna i rezultata.
