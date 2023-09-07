# Test technique

Ce test technique va permettre d'évaluer vos compétences sur l'écosystème .NET Core/Entity Framework Core.
Le C# est le seul language requis pour effectuer ce test. Le temps estimé pour le réaliser est de 2 heures.
(Vous pouvez dépasser ce temps, il n'est pas imparti)
La réussite de ce test sera déterminée par la qualité du code (bonnes pratiques, normes REST, etc...) et non la quantité.

## Contexte

La solution qui vous est fournie est une API Rest en architecture onion. Cette dernière ne fonctionne pas.
Vous devez modifier la solution pour qu'elle fonctionne.

La stack technique est décrite comme telle :

- L'application est codé avec Dotnet 6 et C# 10.
- Un swagger est intégré pour visualiser les routes.
- L'api est servi par un serveur Kestrel.
- L'ORM utilisé est EFCore (Entity Framework Core).
- La BDD est une SqlLite.
- Le framework de test est Xunit et la librairie de mock est Moq.

## Tâches principales

- Modifier le controller 'ProductController' pour que les tests unitaires associés fonctionnent (Vous ne devez pas modifier
  les tests existants dans 'ProductControllerTest').
- Implémenter le 'ProductHandler'.
- Implémenter le 'ProductRepository'.
- CUSTOM : pour faire fonctioner le projet, j'ai du bouger data.db dans le projet WebApi, s'aranger pour que la db reste dans sons projet Persistence
- J'ai aussi changé les id pour retirer les '"' il faut que je le gere, probablement avec https://learn.microsoft.com/en-us/ef/core/modeling/value-conversions?tabs=data-annotations
- Lors des appels API sur 'ProductController', le champ 'Brand' ne doit pas être null.

## Tâches optionelles

- Créer un Controller, Handler et Repository pour l'entité Brand.
- Implémenter les tests unitaires pour le 'ProductHandler'.
- Implémenter les tests unitaires pour le 'ProductRepository'.
