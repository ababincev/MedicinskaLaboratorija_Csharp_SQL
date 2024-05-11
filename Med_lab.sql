USE Med_lab

CREATE TABLE pacijenti(
id_pacijenta INT PRIMARY KEY,
ime VARCHAR(30) NOT NULL,
prezime VARCHAR(30) NOT NULL,
pol VARCHAR(6) NOT NULL,
datum_rodjenja DATE NOT NULL
) 

CREATE TABLE kontakti(
mejl VARCHAR(50),
br_tel VARCHAR(20),
id_pacijenta INT,
CONSTRAINT fk_kontakti_pacijenti
FOREIGN KEY(id_pacijenta) REFERENCES pacijenti(id_pacijenta),
PRIMARY KEY(id_pacijenta)
)

CREATE TABLE referentne_vrednosti(
id INT PRIMARY KEY IDENTITY,
donja_granica FLOAT,
gornja_granica FLOAT,
)

CREATE TABLE analize(
id INT PRIMARY KEY IDENTITY,
ime_analize VARCHAR(50) NOT NULL,
cena INT NOT NULL,
id_ref_vrednost INT,
CONSTRAINT fk_ref_vr_analize
FOREIGN KEY(id_ref_vrednost) REFERENCES referentne_vrednosti(id)
)

CREATE TABLE racun(
id INT PRIMARY KEY IDENTITY,
placen BIT NOT NULL,
iznos INT NOT NULL,
id_pacijenta INT,
spisak_analiza VARCHAR(30) NOT NULL,
CONSTRAINT fk_racun_pacijenti
FOREIGN KEY(id_pacijenta) REFERENCES pacijenti(id_pacijenta),
)

CREATE TABLE zaposleni(
 id INT PRIMARY KEY IDENTITY,
 ime VARCHAR (30) NOT NULL, 
 prezime VARCHAR(30) NOT NULL,
 zanimanje VARCHAR (30) NOT NULL,
 plata INT NOT NULL,
 datum_zaposlenja DATE NOT NULL
)

CREATE TABLE rezultati(
id INT PRIMARY KEY IDENTITY,
id_pacijenta INT,
id_zaposlenog INT,
id_analize INT,
rezultat FLOAT NOT NULL,
CONSTRAINT fk_rezultati_pacijenti
FOREIGN KEY(id_pacijenta) REFERENCES pacijenti(id_pacijenta),
CONSTRAINT fk_rezultati_zaposleni
FOREIGN KEY(id_zaposlenog) REFERENCES zaposleni(id),
CONSTRAINT fk_rezultati_analize
FOREIGN KEY(id_analize) REFERENCES analize(id)
)


INSERT INTO pacijenti (id_pacijenta, ime, prezime, pol, datum_rodjenja)
VALUES (1111, 'Petar', 'Petrovic', 'muski', '2/27/2000'),
	   (2222, 'Nikola', 'Antic', 'muski', '3/14/1978'),
	   (3333, 'Milos', 'Milanovic', 'muski', '8/21/1946'),
	   (4444, 'Marko', 'Markovic', 'muski', '11/3/1999'),
	   (5555, 'Ivana', 'Kostic', 'zenski', '9/24/1996'),
	   (6666, 'Milan', 'Milosevic', 'muski', '2/23/2001'),
	   (7777, 'Janko', 'Jankovic', 'muski', '2/2/1998'),
	   (8888, 'Misa', 'Misic', 'muski', '8/17/1980'),
	   (9999, 'Nikola', 'Nikolic', 'muski', '10/10/1991'),
	   (1010, 'Pero', 'Peric', 'muski', '10/30/1977'),
	   (1011, 'Marko', 'Milosevic', 'muski', '9/12/1950'),
	   (1012, 'Jana', 'Janackovic', 'zenski', '3/8/1993'),
	   (1013, 'Kosta', 'Kostovic', 'muski', '1/13/1963'),
	   (1014, 'Vesna', 'Vesic', 'zenski', '4/27/1996');
SELECT * FROM pacijenti

INSERT INTO kontakti (br_tel, mejl, id_pacijenta)
VALUES ('+381631111111', 'petarpetrovic@gmail.com', 1111),
	   ('+381642222222', 'nikolaantic@gmail.com', 2222),
	   ('+38165333333', 'milosmilanovic@gmail.com', 3333),
	   ('+381644444444', 'markomarkovic@gmail.com', 4444),
	   ('+38162939393', 'ivanakostic@gmail.com', 5555),
	   ('+381645555555', 'milanmilosevic@gmail.com', 6666),
	   ('+38164666666', 'jankojankovic@gmail.com', 7777),
	   ('+381667777777', 'misamisic@gmail.com', 8888),
	   ('+38164888888', 'nikolanikolic@gmail.com', 9999),
	   ('+381649999999', 'peroperic@gmail.com', 1010),
	   ('+38164101010', 'markomilosevic@gmail.com', 1011),
	   ('+381611010101', 'janajanackovic@gmail.com', 1012),
	   ('+381641011101', 'kostakostovic@gmail.com', 1013),
	   ('+38163121212', 'vesnavesic@gmail.com', 1014);

INSERT INTO referentne_vrednosti(donja_granica, gornja_granica)
VALUES (3.5, 6.1),
	   (3.1, 5.5),
	   (1.55, 4.53),
	   (1.03, 1.55),
	   (0.46, 2.27),
	   (2.8, 8.3),
	   (49, 80),
	   (150, 452),
	   (0.000000004, 0.00000001),
	   (0.0000000045, 0.0000000055),
	   (0, 50),
       (0, 0.5),
	   (100, 250),
	   (10, 20)

INSERT INTO analize(ime_analize, cena, id_ref_vrednost)
VALUES ('Glikemija', 130, 1),
	   ('Ukupni holesterol', 165, 2),
	   ('Ldl', 150, 3),
	   ('Hdl', 150, 4),
	   ('Trigliceridi', 150, 5),
	   ('Urea', 300, 6),
	   ('Kreatinin', 160, 7),
	   ('Mokracna kiselina', 145, 8),
	   ('Leukociti', 100, 9),
	   ('Eritrociti', 100, 10),
	   ('D-dimer', 1800, 12),
	   ('Svarljivost', 800, 13),
	   ('Parazitoloski pregled fecesa', 900, 14),
	   ('Kalprotektin', 2800, 11);
SELECT * FROM analize

INSERT INTO racun(placen, id_pacijenta, spisak_analiza, iznos)
VALUES (1, 3333, '3,8', 460),
	   (0, 5555, '14,9,1', 3030),
	   (1, 1013, '4', 610),
	   (1, 4444, '11,12', 2750),
	   (0, 2222, '1,7,11', 2090),
	   (1, 6666, '9,13', 1150);

INSERT INTO Zaposleni (ime, prezime, zanimanje, plata, datum_zaposlenja)
VALUES('Pera', 'Jankovic', 'Higijenicar', 50000, '5/25/2012'),
	  ('Misa', 'Nikolic', 'Laboratorijski tehnicar', 50000, '7/30/2018'),
	  ('Ivan', 'Petrovic', 'Kasir', 40000, '6/15/2021'),
	  ('Mitar', 'Mitrovic', 'Higijenicar', 50000, '8/17/2019'),
	  ('Teodora', 'Zivkovic', 'Biohemicar', 100000, '3/10/2013'),
	  ('Jovana', 'Rajek', 'Biohemicar', 100000, '11/6/2012'),
	  ('Andjela', 'Babincev', 'Biohemicar', 100000, '10/28/2018'),
	  ('Sinisa', 'Zaharijevic', 'Higijenicar', 50000, '1/1/2024'),
	  ('Teodora', 'Spasojevic', 'Kasir', 40000, '12/4/2023'),
	  ('Milica', 'Miljkovic', 'Laboratorijski tehnicar', 50000, '2/23/2015');

INSERT INTO rezultati(id_pacijenta, id_analize, id_zaposlenog, rezultat)
VALUES (3333, 3, 5, 2.01),
       (3333, 8, 7, 549),
	   (5555, 14, 5, 63),
	   (5555, 9, 6, 0.000000006),
	   (1013, 4, 7, 1.67),
	   (4444, 11, 6, 0.9),
	   (4444, 12, 6, 132),
	   (2222, 1, 6, 4),
	   (2222, 7, 5, 53),
	   (2222, 11, 5, 1.15),
	   (6666, 9, 7, 8.92),
	   (6666, 13, 5, 17);
