Salamander 3: Next Engineering Workflow Tool
Open Beta version 0.3.2
© Paul Jeffries 2016-2017
@pnjeffries

Built using the .Nucleus framework also by Paul Jeffries.

Development supported by Ramboll Computational Design
http://www.ramboll.co.uk/

To report bugs and provide feedback, contact paul.jeffries@ramboll.co.uk.


+++SALAMANDER 3+++

Salamander is a structural modelling plugin which links the modelling environment of Rhino and Grasshopper to the analytical capabilities of Autodesk Robot Structual Analysis and Oayss GSA.

The tool is still early in development and is currently in open Beta for community testing and feedback.  Be aware that serious bugs may still be present and that the tool may undergo major alterations in future - at this early stage save file compatibility with future versions is not guaranteed.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


+++INSTALLATION INSTRUCTIONS+++

- Depending on your operating system, you may need to unblock the files before they can be run.  On Windows 10, right click on the .zip, select 'Properties' and check the box marked 'Unblock'.  You are advised to do this prior to unzipping as otherwise you may need to do it to each file individually!
- Unzip the .zip file to somewhere on your local machine.  Keep all files and subfolders together.

RHINO PLUGIN:
- Open Rhino, drag and drop Salamander.Rhino.rhp into the window to install the Rhino Plugin.
- (Optional) Do the same for Salamander.Rhino.Import.rhp and Salamander.Rhino.Export if you want to be able to save and load Salamander files through the normal save/load interface.
- Type in and run the Rhino command 'SalToolbar' to show the Salamander toolbar.

NOTE: If a previous version of the plugin is already installed from a different location you may find that Rhino cannot install the new version as the 'ID is already in use'.  If this happens, simply close and re-open Rhino and drag the plugin file in again - Rhino will have remembered that you tried to load the new version and will defer loading the previous one to make installation possible.  It may be worth deleting any previous versions of Salamander altogether to avoid the possibility of library clashes.

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

v0.2.0 (Open Beta)
- Create Channel and Angle commands/components added.
- Display layers linked to object handle visibility.
- Section profile parameter interface now shown in mm.
- Element orientation tool now works for both linear and panel elements.
- New Element Coordinate Systems Display Layer.
- Panel element orientation transferred to Robot.
- Update options now displayed prior to updating a Robot file.
- New 'Import Model' tool to merge two models together.
- Element releases storage and I/O added.
- Element releases UI, GH component and display layer added.
- .s3b made default savefile extension.
- .s3a made component assembly extension.
- British catalogue L and C sections added.
- Update options exposed in Grasshopper.
- Elements icon on selection panel changes depending on which object types are selected.
- Tolerance for element-node disconnection implemented.
- 'Get all' linear and panel element grasshopper components added.
- Bugfix: OrientateElementToVector was actually orientating towards a point.
- Bugfix: Display refresh slowdown in shaded mode when orientation changed via the slider.
- Bugfix: Non-standard sections not being translated correctly from Robot import.
- Bugfix: Non-sequential Robot nodes were causing geometry corruption on import.
- Bugfix: GSA 2D elements were not being exported from meshes correctly.
- Bugfix: Crash due to not-exactly-planar surfaces failing to convert correctly.
- Bugfix: ID maps for imported Robot files not being correctly stored
- Bugfix: CHS section geometry not being correctly exported to GSA.
- Bugfix: Files opened via the Rhino open menu were being read but not made active.

v0.3.0 (Selection sets)
- Unused Salamander layers now automatically deleted during clean-up.
- Baked extrusions and meshes now contain information about the element they represent in user text.
- Node Numeric ID display layer.
- Element Numeric ID display layer.
- Section profile set-out included in exported offsets.
- Section Family selection combo-box disabled when viewing selected element(s) section.
- Saved element sets implemented.
- Saved node sets implemented.
- Sets table added to model tree.
- Get Element/Node Selection commands/components added.
- Sets export to Robot Groups and GSA Lists.
- Layering order changed to circumvent Rhino pipeline linetype bug.
- New text dialog enabling auto-complete and drop-down selection for command and set names.
- Grasshopper Bool6Ds can now be cast from directions and strings.
- Rhino toolbar: Set command buttons added.
- Rhino toolbar: Section Family creation commands grouped onto separate toolbar.
- Bugfix: Rhino layer names were not being renamed when the bound family name was updated.
- Bugfix: Multiple element selection for manual command inputs not working correctly.
- Bugfix: Generate Pratt Truss component was not outputting the top and bottom node collections.

v0.3.1 (Hotfix)
- Element release interface now indicates when multiple element selections have differing values.
- Distribution now contains a 'Samples' folder, containing truss example definition from 'Basic Grasshopper' tutorial.
- Bugfix: Crash bug when multiple elements with different orientation angles are selected together.

v0.3.2
- Splitting elements will keep releases etc. only at the original ends.
- Data tables initial sizing logic changed.
- Set textual definition parsing implemented.  Set definitions made editable.
- Set definitions may now include references (by name) to other sets of a compatible type.
- New 'Generate Simple Frame' command/component.
- 'Generate Pratt Truss' icon redone.
- Reading basic data from GSA GWA format text files added.
- Basic ETABS exporter implemented.
- Node support display now semi-transparent.
- Node regeneration will now merge coincident unattached nodes.
- Subwindows now owned by main Rhino window.
- Bugfix: Baking elements to extrusions failed when any element without a valid profile existed in the model.
- Bugfix: Crash when re-opening a data table previously closed in edit mode with an invalid entry.
- Bugfix: Pre-export cleanup was deleting unconnected nodes erroneously.

v0.4.0
- Loading interface framework added.
- Creation commands/components added for node loads, gravity loads, linear and panel element UDLs and thermal loads.
- Export of above load types to GWA added.
- Reading and writing of loads from/to Robot implemented.
- Loading display layer added.
- New 'Create Column' command/component.
- New 'Generate Beam Framing' command/component.
- Section catalogue drop-down organised into sub-groups.
- European I and H sections added to the default profile catalogue.
- Angle and channel catalogue Grasshopper components added.
- Grasshopper section profile catalogue components now auto-categorise profiles by prefix (where necessary).
- Text to Section component now outputs the interior void curve of the created profile geometry.
- CreatePanelElement command/component now allows manual drawing of panel edges via points.
- Original panel creation by filling a curve is available via new CreatePanelElementInCurve command/component.