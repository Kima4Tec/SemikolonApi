<h1>Klasser</h1>

## UML Klassediagram
```mermaid
classDiagram
    class Artist {
        int ArtistId
        string? FirstName
        string LastName
        ICollection~ArtistCover~ ArtistCovers
    }

    class ArtistCover {
        int ArtistArtistId
        int CoversCoverId
    }

    class Author {
        int Id
        string FirstName
        string LastName
        ICollection~Book~ Books
    }

    class Book {
        int BookId
        string Title
        DateOnly? PublishDate
        double? BasePrice
        int AuthorId
        Author Author
        Cover Cover
    }

    class Cover {
        int CoverId
        string Title
        bool DigitalOnly
        int BookId
        Book Book
        ICollection~ArtistCover~ ArtistCovers
    }

    %% Relations
    Artist --> ArtistCover : "1..*"
    Cover --> ArtistCover : "1..*"

    Author --> Book : "1..*"
    Book --> Cover : "1"
    Cover --> Book : "1"
    Book --> Author : "1"

    ArtistCover --> Artist : "1"
    ArtistCover --> Cover : "1"
```


## 🧑‍🎨 Artist

| Felt           | Type                       |
| -------------- | -------------------------- |
| `ArtistId`     | `int`                      |
| `FirstName`    | `string` *(valgfri)*       |
| `LastName`     | `string` *(valgfri)*       |
| `ArtistCovers` | `ICollection<ArtistCover>` |


## 🎨 ArtistCover (Join-tabel)

| Felt             | Type  |
| ---------------- | ----- |
| `ArtistArtistId` | `int` |
| `CoversCoverId`  | `int` |

Relationer:

Mange-til-mange mellem Artist og Cover

Hver post forbinder én Artist med ét Cover


## ✍️ Author

| Felt        | Type                |
| ----------- | ------------------- |
| `Id`        | `int`               |
| `FirstName` | `string`            |
| `LastName`  | `string`            |
| `Books`     | `ICollection<Book>` |


## 📖 Book

| Felt          | Type                    |
| ------------- | ----------------------- |
| `BookId`      | `int`                   |
| `Title`       | `string`                |
| `PublishDate` | `DateOnly`              |
| `BasePrice`   | `double`                |
| `AuthorId`    | `int`                   |
| `Author`      | `Author`                |
| `Cover`       | `Cover`                 |


Relationer:

En Book har præcis én Author (1 → mange)

En Book har præcis ét Cover


## 🧾 Cover

| Felt           | Type                       |
| -------------- | -------------------------- |
| `CoverId`      | `int`                      |
| `Title`        | `string` *(valgfri)*       |
| `DigitalOnly`  | `bool`                     |
| `BookId`       | `int`                      |
| `Book`         | `Book`                     |
| `ArtistCovers` | `ICollection<ArtistCover>` |



# Noter
Der kan tilføjes et felt med rabat


<br><br><br>

# APP

## Bog – Indsæt ny

### Påkrævede felter
- **Titel**  
- **Publiceringsdato**  
- **Basispris**  
- **ForfatterFornavn**  
- **ForfatterEfternavn**
- **CoverDigital?** *(true/false)*  

### Valgfrie felter
- **CoverTitel**  
- **CoverKunstnerFornavn**  
- **CoverKunstnerEfternavn**

## Liste af alle bøger

## Søgning og liste på titel eller fornavn eller årstal

