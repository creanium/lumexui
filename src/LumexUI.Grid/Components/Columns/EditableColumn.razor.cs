// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Linq.Expressions;

using LumexUI.Grid.Infra;
using LumexUI.Grid.Services;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace LumexUI.Grid;

public partial class EditableColumn<TGridItem, TProp> : Column<TGridItem, TProp>, IEditableColumn
{
	/// <summary>
	/// Defines an optional editor template for this column. If given, this will be used in cells during edit state.
	/// </summary>
	[Parameter] public RenderFragment<TGridItem>? EditTemplate { get; set; }

	/// <summary>
	/// Specifies whether this column is editable.
	/// </summary>
	/// <remarks>Default is <see langword="true"/></remarks>
	[Parameter] public bool Editable { get; set; } = true;

	/// <summary>
	/// Specifies whether <see cref="LumexNumBox{TValue}"/> shows spin buttons (arrows) in column's editing state.
	/// This setting is only applicable to numeric columns.
	/// </summary>
	/// <remarks>Default is <see langword="false"/></remarks>
	[Parameter] public bool ShowArrows { get; set; } = false;

	[Inject] private IGridNavigationService GridNavigation { get; set; } = default!;

	internal bool IsStringType { get; }
	internal bool IsNumericType { get; }
	internal bool IsDateTimeType { get; }

	internal GridEditContext<TGridItem> GridEditContext => Grid.GridEditContext;
	internal Action<TGridItem, TProp?> ValueUpdate { get; private set; } = default!;

	private LumexTextBox? _textBoxRef;
	private LumexNumBox<double?>? _numBoxRef;

	private bool _navigationRegistered;

	public EditableColumn()
	{
		IsStringType = TypeHelper.IsString( typeof( TProp ) );
		IsNumericType = TypeHelper.IsNumeric( typeof( TProp ) );
		IsDateTimeType = TypeHelper.IsDateTime( typeof( TProp ) );
	}

	internal async Task HandleKeyUpAsync( KeyboardEventArgs args, TGridItem item )
	{
		await GridNavigation.HandleKeyAsync( args, item, onKeyUp: true );
	}

	internal async Task HandleKeyDownAsync( KeyboardEventArgs args )
	{
		await GridNavigation.HandleKeyAsync( args, null, onKeyUp: false );
	}

	internal ValueTask StartEditingItemAsync( TGridItem item )
	{
		if( Editable )
		{
			return GridEditContext.StartEditingItemAsync( this, item );
		}

		return ValueTask.CompletedTask;
	}

	protected override void OnParametersSet()
	{
		if( LastAssignedProperty != Property )
		{
			if( Property.Body is MemberExpression memberExpression )
			{
				ValueUpdate = BuildValueUpdateAction( memberExpression );
			}
		}

		RegisterNavigationActions();

		base.OnParametersSet();
	}

	protected override CellBase<TGridItem> CreateCell( TGridItem item )
	{
		return CellFactory.Create<TGridItem, TProp>( this, item );
	}

	private void RegisterNavigationActions()
	{
		if( Grid.Navigable && Editable && !_navigationRegistered )
		{
			_navigationRegistered = true;

			GridNavigation.RegisterAction( Key.Tab, async ( _ ) => await OnTabKeyDownAsync(), onKeyUp: false );
			GridNavigation.RegisterAction( Key.Enter, async ( obj ) => await OnEnterKeyUpAsync( obj ), onKeyUp: true );
			GridNavigation.RegisterAction( Key.Escape, async ( _ ) => await OnEscapeKeyUpAsync(), onKeyUp: true );
			GridNavigation.RegisterAction( KeyModifier.Shift + Key.Tab, async ( _ ) => await OnShiftTabKeyDownAsync(), onKeyUp: false );
		}
	}

	private async ValueTask OnEnterKeyUpAsync( object? obj )
	{
		if( obj is not TGridItem item )
		{
			return;
		}

		if( !GridEditContext.Editing && Editable )
		{
			await GridEditContext.StartEditingItemAsync( this, item );
		}
		else
		{
			await GridEditContext.StartEditingNextRowCellAsync();
		}
	}

	private async ValueTask OnTabKeyDownAsync()
	{
		if( GridEditContext.Editing && Editable )
		{
			await BlurInputAsync();
			await GridEditContext.StartEditingNextCellAsync( this );
		}
	}

	private async ValueTask OnShiftTabKeyDownAsync()
	{
		if( GridEditContext.Editing && Editable )
		{
			await BlurInputAsync();
			await GridEditContext.StartEditingPrevCellAsync( this );
		}
	}

	private async ValueTask OnEscapeKeyUpAsync()
	{
		if( GridEditContext.Editing && Editable )
		{
			await InvokeAsync( () =>
			{
				GridEditContext.CancelEditingItem();
			} );
		}
	}

	private async ValueTask BlurInputAsync()
	{
		if( _numBoxRef is not null )
			await _numBoxRef.BlurAsync();

		if( _textBoxRef is not null )
			await _textBoxRef.BlurAsync();
	}

	private Action<TGridItem, TProp?> BuildValueUpdateAction( MemberExpression propExpression )
	{
		string propName = propExpression.Member.Name;

		var valueParam = Expression.Parameter( typeof( TProp ), "value" );
		var itemParam = Expression.Parameter( typeof( TGridItem ), "item" );

		var itemProp = Expression.Property( itemParam, propName );
		var assignmentBody = Expression.Assign( itemProp, valueParam );

		var valueAssignmentExpression = Expression.Lambda<Action<TGridItem, TProp?>>( assignmentBody, itemParam, valueParam );
		return valueAssignmentExpression.Compile();
	}
}