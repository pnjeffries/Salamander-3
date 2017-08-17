Salamander 3: Next Engineering Workflow Tool
Open Beta version 0.1.1
© Paul Jeffries 2016-2017
@pnjeffries

Development supported by Ramboll Computational Design
http://www.ramboll.co.uk/


Salamander is a structural modelling plugin for Rhino.

+++INSTALLATION INSTRUCTIONS+++

- Depending on your operating system, you may need to unblock the files before they can be run.  On Windows 10, right click on the .zip, select 'Properties' and check the box marked 'Unblock'.  You are advised to do this prior to unzipping as otherwise you may need to do it to each file individually!
- Unzip the .zip file to somewhere on your local machine.  Keep all files and subfolders together.

RHINO PLUGIN:
- Open Rhino, drag and drop Salamander.Rhino.rhp into the window to install the Rhino Plugin.
- (Optional) Do the same for Salamander.Rhino.Import.rhp and Salamander.Rhino.Export if you want to be able to save and load Salamander files through the normal save/load interface.
- Type in and run the Rhino command 'SalToolbar' to show the Salamander toolbar.

GRASSHOPPER PLUGIN:
- Open Grasshopper, drag and drop Salamander.BasicTools_GH.gha into the window to install the Grasshopper components.  The Grasshopper components rely on the main Rhino plugin so you MUST install that first and keep all files as they come out of the .zip - *do not* move the .gha into the Grasshopper libraries folder on its own as this will break the link.


+++CHANGELOG+++

v0.1.2 (Closed Beta)
- NewDocument command added to blank current Salamander model.  TODO: Prompt to save previous.
- Opening a new Salamander model will delete existing object handles.  TODO: Make optional.
- Element selection properties now include vertex positions, nodes and offsets.
- Element offset display layer added.
- Default scale of node support display changed to 0.5 units
- Salamander handle objects now automatically placed on generated layers
- Element sections may now be baked as extrusion objects (in addition to meshes)
- Angle section profile generation implemented
- Channel section profile types implemented
- Display layer rendering disabled when object handle is hidden
- New Pratt Truss Generation tool/component
- Command to orientate a Linear Element to a direction vector

v0.1.3
- Create Channel and Angle commands/components added.
- Section profile parameter interface now shown in mm.
- Element orientation tool now works for both linear and panel elements.
- New Element Coordinate Systems Display Layer.
- Panel element orientation transferred to Robot.
- Update options now displayed prior to updating a Robot file.
- Bugfix: OrientateElementToVector was actually orientating towards a point.