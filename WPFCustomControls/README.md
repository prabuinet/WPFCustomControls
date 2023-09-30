# WPFCustomControls

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
	contents:
	- Dependency Properties
	- Property Metadata
	- Read Only Properties
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