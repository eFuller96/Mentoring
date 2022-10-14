ToDoItem? FindItem(Guid id) => ToDoItemsDictionary.ContainsKey(id) ? ToDoItemsDictionary[id] : null; //innecesario, sólo para probar ternary conditional

## Dependency Injection additional notes
- Why? Solve testability (uncoupling) and dependency resolving

### Steps
- Extract interface
- Add constructor and private field, inject depedency 
```
private readonly ITodoRepository _todoRepository;

    public ToDoListController(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }
```
- Register interface into service container with an implementation type (in Program.cs)
```
builder.Services.AddSingleton<ITodoRepository,TodoRepository>();
```

### Mocking
- In SetUp: the class we are testing isn't mocked
```
        [SetUp]
        public void SetUp()
        {
            _todoRepository = Substitute.For<ITodoRepository>();
            _todoController = new ToDoListController(_todoRepository);
            _toDoItem = new ToDoItem { Id =  Guid.NewGuid(), IsCompleted = false, Name = "name", Position = 1 };
        }
```
- In test method:
    - Arrange: set what it returns (_todoRepository.GetToDoItems().ReturnsNull();)
    - Act: do whatever
    - Assert: assert the methods inside method tested received the call and the result is type of expected

# 
# Circular dependency
```
builder.Services.AddSingleton<IDictionary<Guid, ToDoItem>, Dictionary<Guid, ToDoItem>>();
```
Da dependencia circular. ¿Por qué? Si vamos a la definición de Dictionary tiene varios constructores:
```
        public Dictionary();
        public Dictionary(IDictionary<TKey, TValue> dictionary);
        public Dictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection);
        public Dictionary(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey>? comparer);
        public Dictionary(IEqualityComparer<TKey>? comparer);
```
Cuando se intente registrar la dependencia, irá al constructor con más dependencias conocidas, que es:

`public Dictionary(IDictionary<TKey, TValue> dictionary);`
    
Pero como justo queríamos registrar IDictionary,Dictionary, y el constructor de Dictionary necesita un IDictionary tenemos una `dependencia circular`.

Por eso, al registrar usaremos: 
`builder.Services.AddSingleton<IDictionary<Guid, ToDoItem>>(new Dictionary<Guid, ToDoItem>());`, donde especificamos que queremos el constructor vacío.

