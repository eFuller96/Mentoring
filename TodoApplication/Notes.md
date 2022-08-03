ToDoItem? FindItem(Guid id) => ToDoItemsDictionary.ContainsKey(id) ? ToDoItemsDictionary[id] : null; //innecesario, sólo para probar ternary conditional
