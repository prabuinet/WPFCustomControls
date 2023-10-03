# WPFCustomControls

##### Introduction to WPF Custom Controls - by Brian Lagunas, Pluralsight


## Anotomy of a custom control
* Custom Control inherits from Control class (we can change it)
* Xaml of the control is placed as a style definition under Themes/Generic.xaml file

## Chosing a base class
* **UIElement** - Most light-weight base class to start from which supports the following: (LIFE)
	- Layout 
	- Input
	- Focus
	- Events
* **FrameworkElement** - Derives from UIElement and adds supports for:
	- Styles
	- Tooltips
	- ContextMenu
	- This is the first base class that takes part of the logical tree (which means it supports):
		- Data Binding
		- Resource Lookup
* **Control** - most common base class, supports:
	- basic properties
	- round background
	- font size, etc.,
* **ContentControl** - basically a contorl with Content Property. For ex. Button
* **HeaderedContentControl** - got a header and content, used for:
    - Expander
	- Tab Control
	- Group box, etc.,
* **ItemsControl** 
	- got Items collection property 
	- They dont support selection
* **Selector**
	- supports index and selection
	- inherited by ListBox, ComboBox, TabControl
* **RangeBase**
	- conrols which displays range of values like sliders, progressbar, (min,max property)
* **Existing Controls**
	- we can also derive from existing control to create a custom control
	- for example derive from a combo box and create a custom one

## Presenters
* Presenters
	- They are placeholder for content
	- Most common presenters are 
	- ContentPresenter - used when control is derived from ContentControl
	- ItemsPresenter - used when control is derived from ItemsControl

## TemplateBinding vs TemplatedParent
* Both are Analogous
* TemplateBinding
	- Markup Extension
	- Optimized binding for templates
	- It has Limited Functionality
		- doesn't work with two-way binding
		- dont't support value converters
		- doesn't work with properties and freezables
		- doesn't work with control template's trigger
* TemplatedParent
	- Full Binding construct using RelativeSource


## Accessing Template Elements
* override OnApplyTemplate - gets called everytime a template is provided to control and constructed
	- ***IMPORTANT: always call base class implementation***
	- get references to named elements in the template in this menthod
* GetTemplateChild - use this mothod to get element by name
* TemplatePartAttribute - tells the Template writer to implement the required elements,		
    informs the parts (elements) that are required to styling the element


## Properties
* contents:
	- Dependency Properties
	- Property Metadata
	- ReadOnly Properties
	- Collection Type Properties
	- Attached Properties
### Dependency Properties
* Why Dependency Property
	- Set Styles
	- Supports DataBinding
	- Set with resource, dynamic resource
	- Support Animation
	- More...
* How to define a dependency property
	- Define a DP Identifier
	- Register property name with property system
	- provide metadata (optional)
	- Create a CLR "wrapper" (optional)
	- // demo in MyControl.cs
### Property Metadata
* What is Property Metadata?
	- specify default values
		```
			public static readonly DependencyProperty TextProperty =
				DependencyProperty.Register("Text", typeof(string), typeof(MyControlDP), 
					new PropertyMetadata("Default Value Text");
		```
	- property change callbacks
		```
			public static readonly DependencyProperty TextProperty =
				DependencyProperty.Register("Text", typeof(string), typeof(MyControlDP), 
					new PropertyMetadata("Default Value Text",
					OnPropertyChangeCallback,);
			private static void OnPropertyChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
			{

			}
		```
	- coercion
		- allows you to change the incoming value
		- ***IMPORTANT this change only affects the control's value, 
		     but not the actual value in data source (which it is databinded to)***
		```
			public static readonly DependencyProperty TextProperty =
				DependencyProperty.Register("Text", typeof(string), typeof(MyControlDP), 
					new PropertyMetadata("Default Value Text",
					OnPropertyChangeCallback,
					OnCoerceValueCallback);
			private static object OnCoerceValueCallback(DependencyObject d, object baseValue)
			{

			}
		```
	- validation
### FrameworkPropertyMetadata
* Gives lot more control than PropertyMetadata
	- Change default data binding mode
	```
        public static readonly DependencyProperty BoldProperty =
            DependencyProperty.Register("Bold", typeof(string), typeof(MyControlDP),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
	```
	- Other Flags we can set:
		- AffectsMeasure
		- AffectsArrange
		- AffectsRender
		- More...
	
### ReadOnly Properties
* It has limited functionality
	- not settable
	- no databinding
	- no animation
	- no validation
	- no inheritance
* why use them?
	- used for state determination
	- used as a property trigger

### Collection Type Properties
* IEnumerable, Observable collections...
	- ***IMPORTANT*** DO NOT provide default value in metadata for collection type properties
	- Provide default value in constructor of custom control or OnApplyTemplate or other area of initialization
	- Why? You dont want to create a sigleton


### Attached Property
* what is attached property
	- Global property for any object
	- No Wrapper
	- Allows us to define attached property on any object other than the defining class
	- common uses:
		- Layout (Ex, DockPanel.Dock, Panel.ZIndex, Canvas.Top)
		- Parent/Child scenario
		- Integration - to support mvvm stuff
		- Visual studio designer support

## Events and Commands
* contents:
	- Understanding Routed Events
	- Custom Routed Events
	- Understanding Routed Commands
	- Custom Routed Commands

* Understanding Routed Events
	- What are they?
		- basically a clr event backed by an instance of a routed event class
		- registed with the wpf eventing system
		- an event can invoke multiple listeners in element tree
	- routing strategies
		- direct - similar to winform events, but supports wpf event triggers etc, ex. MouseEnter
		- bubbling - bubbles up 
		- tunneling - always starts with preview (MouseDownPreview)
	- Why Routed Events
		- Any UIElement can be a listener
		- Common handlers
		- VisualTree communication
		- Event Setter and Event Trigger
* Bubbling
	- to stop bubbling - e.Handled = True; 
	- to capture even the handled events, use AddHandler
	```
		public RoutedEventDemo()
		{
			InitializeComponent();
			this.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(MyHandler), true);
		}

		private void MyHandler(object sender, RoutedEventArgs routedEventArgs)
		{
			Debug.WriteLine("My handler");
		}
	```
* Created Routed Event
	- Register your event with wpf event system
	- Choose routing strategy (direct, bubbling, tunneling)
	- Provide add/remove clr accessors (wrapper)
	- Raise the event
	- Created custom RoutedEventArgs if needed

* Routed Command
	- what are they? special kind of ICommand implementation Execute/CanExecute
	- Execute/CanExecute have no logic 
	- Instead they raise events that traverse the object tree looking for object with command binding
	- Why use them? 
		- decouple from command targets
		- reduce repititive code
		- logic can control enabled state of control
		- associate input gestures (ctrl-x, ctrl-v etc)
* Creating Custom Routed Command
	- Define/Instantiate
	- ExecuteRoutedEventHandler
	- CanExecuteRoutedEventHandler
	- CommandBinding
* ICommandSource - Supports MVVM
	- Command
	- CommandParamenter
	- CommandTarget

