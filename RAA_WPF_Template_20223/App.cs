#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Versioning;
using System.Windows.Markup;

#endregion

namespace RAA_WPF_Template_20223
{
    internal class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication app)
        {
            // 1. Create ribbon tab
            try
            {
                app.CreateRibbonTab("Revit Add-in Academy");
            }
            catch (Exception)
            {
                Debug.Print("Tab already exists.");
            }

            // 2. Create ribbon panel 
            RibbonPanel panel = Utils.CreateRibbonPanel(app, "Revit Add-in Academy", "Revit Tools");

            // 3. Create button data instances
            ButtonDataClass myButtonData = new ButtonDataClass("btnRAA_WPF_Template_20223", "Show/Hide Pane", Command.GetMethod(), Properties.Resources.Blue_32, Properties.Resources.Blue_16, "This is a tooltip");

            // 4. Create buttons
            PushButton myButton = panel.AddItem(myButtonData.Data) as PushButton;
            
            // setup dockable pane
            Globals.myGUID = new Guid(Guid.NewGuid().ToByteArray());
            Globals.myPaneId = new DockablePaneId(Globals.myGUID);

            // register dockable pane
            MyForm myForm = new MyForm();
            app.RegisterDockablePane(Globals.myPaneId, "My dockable pane", myForm as IDockablePaneProvider);

            return Result.Succeeded;

        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }


    }
}
