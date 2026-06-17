# Presenter BETA
*Everything is in developmental stage, so some features might fail to function their intended function*

*There may be mismatch based on the description below to the actual file. Please do understand*
```diff
+ Interactive Workspace: A 16:9 aspect ratio slide surface with professional drop shadows and a dark "Pasteboard" background.
+ Canvas-Based Editing: Replaced static text with an EditorCanvas, allowing free-form placement of multiple elements.
+ Handle-Driven Dragging: Implemented a "Drag Handle" system for text boxes. This allows users to move boxes via a top bar while maintaining the ability to click inside and type.
+ Dynamic Slide System: A WrapPanel that generates interactive thumbnails. Each thumbnail stores its own unique set of canvas elements.
+ Sequential Re-numbering: Logic that automatically re-sequences slide names (View 1, View 2, View 3) when a middle slide is deleted.
+ Boundary UI Hints: Visual "Drag Up/Down" labels that appear automatically when the workspace resizer hits its minimum or maximum height.
+ Themed ContextMenu: A right-click menu styled to match the dark theme, featuring Delete, Pin (Gold highlight), and Undo Delete (Stack-based restoration).
+ Responsive Resizing: A GridSplitter implementation that allows the user to balance the height of the editor vs. the preview area.
+ Hover-Activated Labels: UI buttons that show icons only by default and reveal their text descriptions only when hovered.

- Hard-coded Layouts: Removed negative margins and fixed pixel offsets (e.g., Margin="10,54,0,0") that caused the UI to break on resize.
- Static Text Editor: Removed the single-view TextBox in favor of a multi-element Canvas.
- Event Conflicts: Removed the logic where the TextBox "stole" mouse clicks, which previously prevented users from dragging elements.
- Default Windows Styling: Replaced standard white context menus and blue buttons with custom-templated dark variants.
- Fixed Heights: Replaced static row heights (like 222.04) with proportional * and Auto definitions.
```
