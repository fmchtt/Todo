# Queries explanation and listing

### Board

1. [boards]
Refers to all boards owned by the user, the type of the query is ResumedBoard[]

2. [board, {id}]
Refers to a single board, the type of the query is ExpandedBoard

### Item

1. [itens]
Refers to all itens owned by the user, the type of the query is ExpandedItem[]

2. [item, {id}]
Refers to a single item, the type of the query is ExpandedItem[]

## Creation and Updates

To update or create an column, the query *Board.1* should be updated to set the column

To update or create an item, the queries *Item.1*, *Item.2* and *Board.2* should be updated to set the item (if done was updated, the *Board.1* should be updated to set the count of done/undone)

To update or create an board, the query *Board.1* should be updated to set the board (if the board was updated and the cache contains the board, the board should be updated on the *Board.2* query)

## Deletion

The deletion always follows the same steps as the update but removing insted of updating