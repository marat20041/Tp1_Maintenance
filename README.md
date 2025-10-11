# TP1-Maintenance-Logiciel
## Role dans l'équipe
| Role    | Nom, Prénom | Tache Effectuer  |
| -------- | ------- | ------- | 
| Team-Lead  | N'Goran, Beau-Séjour Djatti    | Ajout de la fonctionnalite d'annulation, Refactorisation, Ecriture du README.md |
| QA | Werbrouck, Nicolas     | Reglement des codes smells, Refactorisation du code, Programation defensive, Configuration de fichier Json, Creation des tests unitaitres, Reglement de bogue |
| Programmeur    | Sargsyan, Marat    | |

Chaque membre de l'équipe on un role attribué, toutefois, il n'est pas imporbable qu'il ait eu de l'entraide entre les membres de l'équipe

---

## Installation 
### 1. Clonez le projet
```
git clone https://github.com/marat20041/Tp1_Maintenance
cd Tp1-Maintenance
```

### 2. Ouvrez votre dossier de projet dans le terminal

```sh
cd "C:\chemin\vers\le\fichier\projet\TP1-Maintenance-Logiciel"
```

### 3. Si vous n'avez pas de fichier .csproj, créez-en un

```sh
dotnet new console --output .
```

### 4. Restaurez les dépendances

```sh
dotnet restore
```

### 5. Compilez le projet

```sh
dotnet build
```

### 6. Exécutez le projet

```sh
dotnet run
```

### 6.1 Executez les test unitaires
```sh
dotnet test
```

## Changement effectuer au code

_Mettre la liste des changements effectuer ainsi que de la justification de ces derniers Ex : Capitalisation des noms de fonctions, ce changement a été effectuer afin de respecter els conventions de codes de C#_

1- Modification du type de la variable "Phone" (de int à string)
    Permet la prise en charge des numéros débutant par 0.
    Facilite la vérification et la validation des numéro téléphone 
  
2- Class Teacher,Student,Receptionist,Principal,SchoolMember
  * Refactorisation des différentes classes ()/Codes smells
    Implémentation correct de l'Héritage par l'ajout de ":base(variables héritées du parent)"
    Respect des conventions d'écriture en C#: (Débuter les variables publiques par des Majuscules,Donner des noms significatifs et simples ,Écriture des variables privées avec "_")
    
  Cette reécriture du code vise le respect des conventions d'écriture en C#, facilite la lecture, la compréhension, et la modification du code.
  
  * Programmation défensive:
   Dans le but d'assurer la sécurité et le bon fonctionnement du programme, la programmation défensive vise à baliser les informations pendant l'implémentation et à établir les conditions de fonctionnement de        l'application.

3- Class Programm
   * Refactorisation et codes smells
     Les actions menées dans cette classe, vise à faciliter la lecture du code, la maintenance et l'évolution.
         - Un dossier "functionality" a été créé afin d'abriter toutes les fonctionalités du programme. Ces dernières ayant été préalablement subdisvisées en classe et définit par leur fonction respective.
     
4- Ajout de nouvelles fonctionalitées 
    * Annuler la dernière action d'ajout du membre :
    * Anuler la dernière action de paie de certains membre:
    * Permettre de définir le salaire d'un professeur
    * Ajout d'un fichier JSON
    
5- Ajout d'un fichier xUnit
    créer un environnement de test afin de s'assurer de la bonne fonctionabilité du programme en général et/ou des composantes du programme
6- Fichier JSON
    Afin d'éviter de stocker en dur les informations pendant l'implémentation du programme, le fichier JSON dans ses attributs, offre également la possiblité de stocker du data. 
7- README
