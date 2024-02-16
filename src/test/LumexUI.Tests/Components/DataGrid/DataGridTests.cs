// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Grid;
using LumexUI.Grid.Services;
using LumexUI.TestComponents.DataGrid;

using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.Extensions.DependencyInjection;

namespace LumexUI.Tests.Components;

public class DataGridTests : TestContext
{
	/// <summary>
	/// The testing environemnt setup for components under test.
	/// </summary>
	public DataGridTests()
	{
		Services.AddSingleton<ICellFactory, CellFactory>();
		Services.AddSingleton<IGridNavigationService, GridNavigationService>();

		//var m1 = JSInterop.SetupModule( "", _ => true );
		var m2 = JSInterop.SetupModule( "./_content/LumexUI.Grid/Components/LumexGrid.razor.js" );

		m2.SetupModule( "init", _ => true );
		m2.SetupVoid( "inputAutofocus", _ => true );
	}

	#region DataGrid Tests
	[Fact]
	public void DataGrid_WithData_ShowsAllColumns()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithData>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithData.TestModel>>();

		// Assert
		cut.FindComponents<Column<DataGridWithData.TestModel, object>>().Should().HaveCount( 2 );
	}

	[Fact]
	public void DataGrid_WithData_ShowsAllRows()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithData>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithData.TestModel>>();

		// Assert
		cut.FindAll( "tbody tr" ).Should().HaveCount( 3 );
	}

	[Fact]
	public void DataGrid_WithDataAndDataSource_ThrowsInvalidOperationEx()
	{
		// Arrange, Act, Assert
		Assert.Throws<InvalidOperationException>( () =>
		{
			var assert = RenderComponent<DataGridWithDataAndDataSource>();
		} );
	}

	[Fact]
	public void DataGrid_WithoutData_ShowsNoRecords()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithoutData>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithoutData.TestModel>>();

		// Assert
		cut.Find( "tr.lumex-grid-row-norecords" ).Should().NotBeNull();
	}

	[Fact]
	public void DataGrid_WithNoRecordsTemplate_ShowsThisTemplate()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithNoRecordsTemplate>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithNoRecordsTemplate.TestModel>>();

		// Assert
		cut.Find( "tr.lumex-grid-row-norecords" ).TextContent.Trim().Should().Be( "No records to display!" );
	}

	[Fact]
	public void DataGrid_WithLoadingState_ShowsLoader()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithLoadingState>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithLoadingState.TestModel>>();

		// Assert
		cut.Find( "tr.lumex-grid-row-loader" ).Should().NotBeNull();
	}

	[Fact]
	public void DataGrid_WithLoaderTemplate_ShowsThisTemplate()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithLoaderTemplate>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithLoaderTemplate.TestModel>>();

		// Assert
		cut.Find( "tr.lumex-grid-row-loader" ).TextContent.Trim().Should().Be( "Waiting for minions to show the data..." );
	}

	[Fact]
	public void DataGrid_WithVirtualized_HasVirtualize()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithVirtualized>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithVirtualized.TestModel>>();

		// Assert
		cut.HasComponent<Virtualize<(int, DataGridWithVirtualized.TestModel)>>().Should().BeTrue();
	}

	[Fact]
	public void DataGrid_WithClass_ContainsProvidedCss()
	{
		// Arrange, Act
		var @class = "custom-css";
		var cut = RenderComponent<LumexGrid<object>>( p => p.Add( p => p.Class, @class ) );

		// Assert
		cut.Find( ".lumex-datagrid" ).ClassName.Should().Contain( @class );
	}

	[Fact]
	public void DataGrid_WithHoverable_ContainsCorrespondingCss()
	{
		// Arrange, Act
		var cut = RenderComponent<LumexGrid<object>>( p => p.Add( p => p.Hoverable, true ) );

		// Assert
		cut.Find( "table" ).ClassName.Should().Contain( "lumex-grid--hover" );
	}

	[Fact]
	public void DataGrid_WithSingleSelectionOnCheckboxChange_SelectsSingleItem()
	{
		// Arrange
		var dataGrid = RenderComponent<DataGridWithSingleSelection>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithSingleSelection.TestModel>>();

		// Act 1: Checkbox Select
		cut.FindAll( "tbody input" )[1].Change( true );
		cut.FindAll( "tbody input" )[0].Change( true );

		// Assert 1: Checkbox Select
		cut.Instance.SelectedItems.Should().HaveCount( 1 );
		cut.Instance.SelectedItems.Should().Contain( cut.Instance.Data!.First() );

		// Assert 2: Header Checkbox Existence
		cut.Find( "thead th" ).ChildNodes.Should().BeEmpty();
	}

	[Fact]
	public void DataGrid_WithMultipleSelectionOnCheckboxChange_SelectsMultipleItems()
	{
		// Arrange
		var dataGrid = RenderComponent<DataGridWithMultipleSelection>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithMultipleSelection.TestModel>>();

		// Act 1: Header Checkbox Select
		cut.Find( "thead input" ).Change( true );

		// Assert 1: Header Checkbox Select
		cut.Instance.SelectedItems.Should().HaveCount( 3 );

		// Act 2: Header Checkbox Deselect
		cut.Find( "thead input" ).Change( false );

		// Assert 2: Header Checkbox Deselect
		cut.Instance.SelectedItems.Should().BeEmpty();

		// Act 3: Checkbox Select
		cut.FindAll( "tbody input" )[0].Change( true );
		cut.FindAll( "tbody input" )[1].Change( true );

		// Assert 3: Checkbox Select
		cut.Instance.SelectedItems.Should().HaveCount( 2 );
	}

	[Fact]
	public void DataGrid_WithSelectedItemsOnCheckboxChange_DeselectsSingleItem()
	{
		// Arrange
		var dataGrid = RenderComponent<DataGridWithSelectedItems>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithSelectedItems.TestModel>>();

		// Act
		cut.FindAll( "tbody input" )[0].Change( false );
		cut.FindAll( "tbody input" )[1].Change( false );

		// Assert
		cut.Instance.SelectedItems.Should().HaveCount( 1 );
		cut.Instance.SelectedItems.Should().Contain( cut.Instance.Data!.Last() );
	}

	[Fact]
	public void DataGrid_WithSingleSelectionOnRowClick_SelectsSingleItem()
	{
		// Arrange
		var dataGrid = RenderComponent<DataGridWithSingleSelection>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithSingleSelection.TestModel>>();

		// Act
		cut.FindAll( "tbody tr" )[1].Click();
		cut.FindAll( "tbody tr" )[0].Click();

		// Assert
		cut.Instance.SelectedItems.Should().HaveCount( 1 );
		cut.Instance.SelectedItems.Should().Contain( cut.Instance.Data!.First() );
	}

	[Fact]
	public void DataGrid_WithMultipleSelectionOnRowClick_SelectsMultipleItems()
	{
		// Arrange
		var dataGrid = RenderComponent<DataGridWithMultipleSelection>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithMultipleSelection.TestModel>>();

		// Act
		cut.FindAll( "tbody tr" )[0].Click();
		cut.FindAll( "tbody tr" )[1].Click();

		// Assert
		cut.Instance.SelectedItems.Should().HaveCount( 2 );
	}

	[Fact]
	public void DataGrid_WithAnySelectionOnRowClick_DeselectsSingleItem()
	{
		// Arrange
		var dataGrid = RenderComponent<DataGridWithSelectedItems>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithSelectedItems.TestModel>>();

		// Act
		cut.FindAll( "tbody tr" )[0].Click();
		cut.FindAll( "tbody tr" )[1].Click();

		// Assert
		cut.Instance.SelectedItems.Should().HaveCount( 1 );
		cut.Instance.SelectedItems.Should().Contain( cut.Instance.Data!.Last() );
	}

	[Fact]
	public void DataGrid_WithSelectedItemsAndSingleSelection_DoesNotSelectCheckboxes()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithSelectedItemsAndSingleSelection>();
		var dataGridComp = dataGrid.FindComponent<LumexGrid<DataGridWithSelectedItemsAndSingleSelection.TestModel>>();

		var cut = dataGridComp.FindComponents<LumexCheckbox>();

		// Assert
		cut[0].Instance.Value.Should().BeFalse();
		cut[1].Instance.Value.Should().BeFalse();
		cut[2].Instance.Value.Should().BeFalse();
	}

	[Fact]
	public void DataGrid_WithNoneSelection_DoesNotSelectItem()
	{
		// Arrange
		var dataGrid = RenderComponent<DataGridWithNoneSelection>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithNoneSelection.TestModel>>();

		// Act
		cut.Find( "tbody tr" ).Click();

		// Assert
		cut.Instance.SelectedItems.Should().BeEmpty();
	}

	[Fact]
	public void DataGrid_WithOnRowRender_SetsProvidedCssOnRows()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithOnRowRender>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithOnRowRender.TestModel>>();

		// Assert
		cut.Find( "tbody tr" ).ClassName.Should().Contain( "custom-css" );
		cut.Instance.OnRowRender.Should().NotBeNull();
	}

	[Fact]
	public void DataGrid_WithOnRowClick_EmitsGridRowClickedEvent()
	{
		// Arrange
		var dataGrid = RenderComponent<DataGridWithOnRowClick>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithOnRowClick.TestModel>>();

		dataGrid.Instance.GridRowClickArgs.Should().BeNull();
		cut.Instance.OnRowClick.HasDelegate.Should().BeTrue();

		// Act
		cut.Find( "tbody tr" ).Click();

		// Assert
		dataGrid.Instance.GridRowClickArgs!.Item.Should().Be( cut.Instance.Data!.First() );
		dataGrid.Instance.GridRowClickArgs!.RowIndex.Should().Be( 2 );
		dataGrid.Instance.GridRowClickArgs!.MouseEventArgs.Should().NotBeNull();
	}

	[Fact]
	public void DataGrid_WithOnEdit_EmitsGridEditEvent()
	{
		// Arrange
		var dataGrid = RenderComponent<DataGridWithOnEdit>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithOnEdit.TestModel>>();

		dataGrid.Instance.GridEditEventArgs.Should().BeNull();

		// Act
		cut.Find( "tbody td" ).Click();

		// Assert
		cut.Instance.OnEdit.HasDelegate.Should().BeTrue();
		dataGrid.Instance.GridEditEventArgs!.Item.Should().Be( cut.Instance.Data!.First() );
		dataGrid.Instance.GridEditEventArgs!.ColumnName.Should().Be( cut.Instance.RenderedColumns[0].Title );
	}

	//[Fact]
	//public void DataGrid_WithOnUpdate_ReceivesGridUpdateEvent()
	//{
	//	// Arrange
	//	var dataGrid = RenderComponent<DataGridWithOnUpdate>();
	//	var cut = dataGrid.FindComponent<LumexGrid<DataGridWithOnUpdate.TestModel>>();

	//	dataGrid.Instance.GridUpdateEventArgs.Should().BeNull();
	//	cut.Instance.OnUpdate.HasDelegate.Should().BeTrue();

	//	// Act
	//	cut.Find( "tbody > tr > td" ).Click();

	//	// Assert
	//	dataGrid.Instance.GridUpdateEventArgs!.IsValid.Should().BeTrue();
	//}

	[Fact]
	public void DataGrid_WithColumnTemplate_ShowsCellWithThisTemplate()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithColumnTemplate>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithColumnTemplate.TestModel>>();

		// Assert
		cut.FindAll( "tbody td" )[0].TextContent.Trim().Should().Contain( "This is custom cell template" );
		cut.FindAll( "tbody td > div" )[0].FirstChild!.NodeName.Should().Be( "SPAN" );

		cut.FindAll( "tbody td" )[1].TextContent.Trim().Should().Contain( "This is another custom cell template" );
		cut.FindAll( "tbody td > div" )[1].FirstChild!.NodeName.Should().Be( "SPAN" );
	}

	[Fact]
	public void DataGrid_WithColumnHeaderTemplate_ShowsHeaderCellWithThisTemplate()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithColumnHeaderTemplate>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithColumnHeaderTemplate.TestModel>>();

		// Assert
		cut.FindAll( "thead th" )[0].TextContent.Trim().Should().Contain( "This is custom header template" );
		cut.FindAll( "thead th" )[0].FirstChild!.NodeName.Should().Be( "H5" );

		cut.FindAll( "thead th" )[1].TextContent.Trim().Should().Contain( "This is another custom header template" );
		cut.FindAll( "thead th" )[1].FirstChild!.NodeName.Should().Be( "H5" );
	}

	[Fact]
	public void DataGrid_WithColumnTitle_ShowsThisTitleAsHeader()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithColumnTitle>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithColumnTitle.TestModel>>();

		// Assert
		cut.FindAll( "thead th" )[0].TextContent.Should().Be( "Custom Title" );
		cut.FindAll( "thead th" )[1].TextContent.Should().Be( "Price" );
	}

	[Fact]
	public void DataGrid_WithColumnWidth_SetsProvidedWidthToColumn()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithColumnWidth>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithColumnWidth.TestModel>>();

		// Assert
		cut.FindAll( "colgroup col" )[0].Attributes["width"]!.Value.Should().Contain( "80%" );
		cut.FindAll( "colgroup col" )[1].Attributes["width"]!.Value.Should().Contain( "100%" );
	}

	[Fact]
	public void DataGrid_WithColumnClass_SetsProvidedCssToEntireColumn()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithColumnClass>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithColumnClass.TestModel>>();

		// Assert
		cut.FindAll( "thead th" )[0].ClassName.Should().Contain( "custom-css" );
		cut.FindAll( "thead th" )[1].ClassName.Should().NotContain( "custom-css" );

		cut.FindAll( "tbody td" )[0].ClassName.Should().Contain( "custom-css" );
		cut.FindAll( "tbody td" )[1].ClassName.Should().NotContain( "custom-css" );
	}

	[Fact]
	public void DataGrid_WithColumnHeaderClass_SetsProvidedCssToHeader()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithColumnHeaderClass>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithColumnHeaderClass.TestModel>>();

		// Assert
		cut.FindAll( "thead th" )[0].ClassName.Should().Contain( "custom-css" );
		cut.FindAll( "thead th" )[1].ClassName.Should().NotContain( "custom-css" );

		cut.FindAll( "tbody td" )[0].ClassName.Should().NotContain( "custom-css" );
		cut.FindAll( "tbody td" )[1].ClassName.Should().NotContain( "custom-css" );
	}

	[Fact]
	public void DataGrid_WithColumnVisibleFalse_DoesNotShowUp()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithColumnVisible>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithColumnVisible.TestModel>>();

		// Assert
		cut.Instance.RenderedColumns.Should().HaveCount( 1 );
	}

	[Fact]
	public void DataGrid_WithColumnOnCellRender_SetsProvidedCssOnCells()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithColumnOnCellRender>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithColumnOnCellRender.TestModel>>();

		// Assert
		var cells = cut.FindAll( "tbody td[aria-colindex='1']" );

		cells[0].ClassName.Should().Contain( "custom-css", because: "We want CSS to be applied only on the cells where the letter 'a' is not present." );
		cells[1].ClassName.Should().NotContain( "custom-css" );
		cells[2].ClassName.Should().Contain( "custom-css" );
	}

	[Fact]
	public void DataGrid_WithFilterable_FiltersDataAccordingly()
	{
		// Arrange
		var dataGrid = RenderComponent<DataGridWithFilterable>();
		var cut = dataGrid.FindComponent<LumexGrid<DataGridWithFilterable.TestModel>>();

		// Act 1: With lowercase
		cut.Find( ".lumex-toolbar .filterbox input" ).Input( "k" );

		// Assert 1: With lowercase
		cut.Instance.CurrentData.Should().HaveCount( expected: 1, because: "There is only 1 element that contains 'k/K' letter." );

		// Act 2: With uppercase
		cut.Find( ".lumex-toolbar .filterbox input" ).Input( "A" );

		// Assert 2: With uppercase
		cut.Instance.CurrentData.Should().HaveCount( expected: 2, because: "There is only 2 elements that contain 'a/A' letter." );
	}
	#endregion

	#region DataGridColumn Tests
	[Fact]
	public void DataGridColumn_Default_InitializesCorrectly()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridWithData>();
		var dataGridComp = dataGrid.FindComponent<LumexGrid<DataGridWithData.TestModel>>();

		var cut = dataGridComp.FindComponents<Column<DataGridWithData.TestModel, object>>();

		// Assert
		cut[0].Instance.Index.Should().Be( 0 );
		cut[0].Instance.Visible.Should().BeTrue();
		cut[0].Instance.Title.Should().Be( "Name" );

		cut[1].Instance.Index.Should().Be( 1 );
		cut[1].Instance.Visible.Should().BeTrue();
		cut[1].Instance.Title.Should().Be( "Price" );
	}
	#endregion

	#region DataGridEditableColumn Tests
	[Fact]
	public void DataGridEditableColumn_Default_InitializesCorrectly()
	{
		// Arrange, Act
		var dataGrid = RenderComponent<DataGridEditableColumnDefault>();
		var dataGridComp = dataGrid.FindComponent<LumexGrid<DataGridEditableColumnDefault.TestModel>>();

		var cut = dataGridComp.FindComponent<EditableColumn<DataGridEditableColumnDefault.TestModel, string>>();
		var cut2 = dataGridComp.FindComponent<EditableColumn<DataGridEditableColumnDefault.TestModel, double>>();

		// Assert
		cut.Instance.IsStringType.Should().BeTrue();
		cut.Instance.Editable.Should().BeTrue();
		cut.Instance.GridEditContext.Should().NotBeNull();
		cut.Instance.GridEditContext.Editing.Should().BeFalse();

		cut2.Instance.IsNumericType.Should().BeTrue();
		cut2.Instance.Editable.Should().BeTrue();
		cut2.Instance.GridEditContext.Should().NotBeNull();
		cut2.Instance.GridEditContext.Editing.Should().BeFalse();
	}

	[Fact]
	public void DataGridEditableColumn_WhenCellIsClicked_EntersEditingMode()
	{
		// Arrange
		var dataGrid = RenderComponent<DataGridEditableColumnDefault>();
		var dataGridComp = dataGrid.FindComponent<LumexGrid<DataGridEditableColumnDefault.TestModel>>();

		var cut = dataGridComp.FindComponent<EditableColumn<DataGridEditableColumnDefault.TestModel, string>>();

		// Act
		dataGridComp.Find( "tbody td" ).Click();

		// Assert
		cut.Instance.GridEditContext.Editing.Should().BeTrue();
	}

	[Fact]
	public void DataGridEditableColumn_WhenCellIsClicked_ShowsAppropriateEditor()
	{
		// Arrange
		var dataGrid = RenderComponent<DataGridEditableColumnDefault>();
		var dataGridComp = dataGrid.FindComponent<LumexGrid<DataGridEditableColumnDefault.TestModel>>();

		// Act 1: Start editing column of text type
		dataGridComp.Find( "tbody td[aria-colindex='1']" ).Click();

		// Assert 1: Start editing column of text type
		dataGridComp.Find( "tbody td[aria-colindex='1']" ).TextContent.Trim().Should().BeEmpty();
		dataGridComp.HasComponent<LumexTextBox>().Should().BeTrue();

		// Act 2: Start editing column of numeric type
		dataGridComp.Find( "tbody td[aria-colindex='2']" ).Click();

		// Assert 2: Start editing column of numeric type
		dataGridComp.Find( "tbody td[aria-colindex='2']" ).TextContent.Trim().Should().BeEmpty();
		dataGridComp.HasComponent<LumexNumBox<double?>>().Should().BeTrue();
	}

	[Fact]
	public void DataGridEditableColumn_WithEditTemplate_ShowsThisTemplate()
	{
		// Arrange
		var dataGrid = RenderComponent<DataGridEditableColumnWithEditTemplate>();
		var dataGridComp = dataGrid.FindComponent<LumexGrid<DataGridEditableColumnWithEditTemplate.TestModel>>();

		var cut = dataGridComp.FindComponent<EditableColumn<DataGridEditableColumnWithEditTemplate.TestModel, string>>();

		// Act
		dataGridComp.Find( "tbody td[aria-colindex='1']" ).Click();

		// Assert
		cut.Instance.EditTemplate.Should().NotBeNull();
		dataGridComp.Find( "tbody td.lumex-grid-cell.lumex-edit > div" ).FirstChild!.NodeName.Should().Be( "INPUT" );
	}
	#endregion
}
