# Gestion des processus — Guide d’utilisation

Ce dépôt contient deux applications console :
- `ConsoleHelloWorld` : affiche un message puis attend un délai.
- `ProcessusParent` : lance `ConsoleHelloWorld`, puis ouvre des processus Windows (Explorer/Notepad).

## Prérequis
- .NET SDK (version compatible avec `net10.0`)
- Windows (usage de `explorer.exe` et `notepad.exe`)

## Construire la solution
```powershell
dotnet build Gestion-des-processus-2.sln
```

## Utiliser ConsoleHelloWorld
Arguments :
- `name` : nom à afficher
- `delay_ms` : durée avant fermeture automatique (en millisecondes)

Exemple :
```powershell
dotnet run --project ConsoleHelloWorld -- "Alice" 2000
```

## Utiliser ProcessusParent
Lance `ConsoleHelloWorld`, ouvre l’Explorateur dans `C:\Windows` et ouvre un fichier texte dans Notepad.
```powershell
dotnet run --project ProcessusParent
```

### Comportement attendu
- Création d’un fichier `note-q5.txt` à la racine du repo.
- Ouverture de `C:\Windows` via `explorer.exe`.
- Ouverture de `note-q5.txt` et de `C:\Windows\win.ini` via l’éditeur par défaut.
