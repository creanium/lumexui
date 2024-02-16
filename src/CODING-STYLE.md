## ✨ C# Naming & Coding Conventions

### Naming Conventions for type members
#### Naming of Fields
- Use leading underscore `_` and `camelCase` for `private` field names

	```
	❌ private bool someField;
	❌ private bool SomeField;
	❌ private bool _SomeField;
	
	✔️ private bool _someField;
	```
	
#### Naming of Properties
- Use `PascalCase` for any property name

	```
	❌ public bool someProperty { get; set; }
	
	✔️ public bool SomeProperty { get; set; }
	```
	
#### Naming of Constants
- Use `PascalCase` for any constant name
  
	```
	❌ public const string SOME_CONST = "Some constant!";
	
	✔️ public const string SomeConst = "Some constant!";	
	```
---
### Coding Convention for type members
#### Coding of Fields
- Fields should always be `private`, otherwise create a property

	```
	❌ protected bool _someField;
	
	✔️ protected bool SomeProperty { get; set; }
	```
- Mark field as `readonly` whenever possible if its value will never change
- Use fields for storing data in the scope of a class, otherwise create a property

#### Coding of Properties
- Create get-only properties whenever possible if the caller should not be able to change the value of the property

#### Class Members Order
- Constants
- Parameters `[Parameter]` _(Blazor Component)_
- Services `[Inject]` _(Blazor Component)_
- Properties
- Fields
- Constructors
- Methods
- Classes / Structs / Enums

Members should be ordered by an access modifiers:
- public (except `Dispose` methods, these are always in the end of the file)
- protected internal
- internal
- protected
- private protected
- private
