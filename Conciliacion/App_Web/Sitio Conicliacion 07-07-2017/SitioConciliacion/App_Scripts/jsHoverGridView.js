		var gridview = null;
        var gridviewFreeze = null;
        var selectedRowIndex = -1;

        function RowMouseOver(rowIndex) {
            if (selectedRowIndex == rowIndex) return;
            gridview[0].rows[rowIndex + 1].className = 'filaEncima';
        }

        function RowMouseOut(rowIndex) {
            if (selectedRowIndex == rowIndex) return;
            gridview[0].rows[rowIndex + 1].className = 'filaNormal';
            
        }

        function RowSelect(rowIndex) {
            if (selectedRowIndex == rowIndex) return;
            RowReset(selectedRowIndex);
            selectedRowIndex = rowIndex;
            gridview[0].rows[rowIndex + 1].className = 'filaSeleccionada';


        }
        function RowReset(rowIndex) {
            gridview[0].rows[rowIndex + 1].className = 'filaNormal';
        } 