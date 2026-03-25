using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogDatabaseSO", menuName = "Dialog System/DialogDatabaseSO")]
public class DialogDatabaseSO : ScriptableObject
{
    public List<DialogSO> dialogs = new List<DialogSO>();
    private Dictionary<int, DialogSO> dialogsById;

    public void Initilize()
    {
        dialogsById = new Dictionary<int, DialogSO>();

        foreach (var dialog in dialogs)
        {
            if (dialog != null) dialogsById[dialog.id] = dialog;
        }
    }

    public DialogSO GetDialogById(int id)
    {
        if (dialogsById == null) Initilize();
        if (dialogsById.TryGetValue(id, out DialogSO dialog)) return dialog;
        return null;
    }
}
