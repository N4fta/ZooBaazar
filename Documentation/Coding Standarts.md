# Variables
- Properties (get sets) are Pascal Case
- privateVariables are camel Case
- Classes & Methods are Pascal Case
- Forms are Pascal Case and in the format `Name + "Form"`
- Mostly use Properties unless you need a private variable

# Comments
- Comments must explain what isn't obvious or clarify complicated code.
- Dont use comments in the style of : "This does 1+1"

# Functions/Methods
- Single Responsibility & only abstract a function if it is used in multiple parts of the code (It appeared multiple times)
- First check for incorrect conditions then procceed into the actual code
- Have some input validation at the start, return "-1" or False or "" if validation fails
- Dont use bandage solutions, this can cause unexpected errors for other code down the line
- When returning Collections return Arrays unless necessary
- When possible return a bool

# Structure
- If the code is above 300 lines check if you can abstract some code into a new class
- ToString() returns only the Name
- Every Class that manages a list of instances of a Class is called `NameOfTheClass + "Manager"` e.g. TaskManager
- All enums go into the MyEnum.cs
- Every Interface & Class has its own cs file

# WinForms
- Form Constructors are passed a Manager and possibly an object to Edit or View
- Adding, Editing & Viewing an instance of a class opens another Form
- Elements will have a name formed by either:
    - Removing vowels e.g. Label => lbl
    - First letter of every word e.g. NumericUpDown => nud
    Followed by the Name of the variable you are editing e.g. lblName; lbAnimals; nudAge;
    Common Elements:
    - Label: lbl
    - ListBox: lb
    - NumericUpDown: nud
    - ComboBox: cb
    - TextBox: tb
    - Button: btn
    - StatusStrip: ss
    - TabControl: tc
    - TabPage: tp
    - DateTimePicker: dtp
    If two have the same name add the name of the form/parent element e.g. lblNameAnimalForm
- Default Text is "Segoe UI, 16.2pt"
- Minimun Form size is around 1280 720
- New Forms open at the center of the parent Form
- When you close a Form the parent Form opens 

# WebPages

# Database