using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIREditor
{
    class EditManager
    {
        const int UNDO_STACK_SIZE = 50;
        List<BeatInfo> newestRecord;
        LinkedList<KeyValuePair<string,List<BeatInfo>>> undoStack;
        LinkedListNode<KeyValuePair<string, List<BeatInfo>>> undoPtr, redoPtr;
        public bool CanUndo
        {
            get { return undoPtr != null; }
        }
        public string LastUndoInfo
        {
            get { return CanUndo ? undoPtr.Value.Key : ""; }
        }
        public bool CanRedo
        {
            get { return undoPtr != redoPtr; }
        }
        public string LastRedoInfo
        {
            get { return CanRedo ? (undoPtr==null?undoStack.First:undoPtr.Next).Value.Key : ""; }
        }
        public EditManager()
        {
            Reset();
        }
        public void Reset()
        {
            undoStack = new LinkedList<KeyValuePair<string, List<BeatInfo>>>();
            undoPtr = redoPtr = null;
        }
        public void BeforePreformEdit(SongInfo info,string commandName)
        {
            while(undoPtr!=redoPtr)
            {
                var preNode = redoPtr.Previous;
                undoStack.Remove(redoPtr);
                redoPtr = preNode;
            }
            undoPtr = new LinkedListNode<KeyValuePair<string, List<BeatInfo>>>(
                new KeyValuePair<string, List<BeatInfo>>(commandName, GetClone(info.Beats)));
            redoPtr = undoPtr;

            undoStack.AddLast(undoPtr);
            if (undoStack.Count > UNDO_STACK_SIZE) undoStack.RemoveFirst();
        }
        public List<BeatInfo> Undo(SongInfo info)
        {
            if(CanUndo)
            {
                if(!CanRedo)
                {
                    newestRecord = info.Beats;
                }
                List<BeatInfo> result = undoPtr.Value.Value;
                if (undoPtr == undoStack.First)
                {
                    undoPtr = null;
                }
                else undoPtr = undoPtr.Previous;
                return result;
            }
            return null;
        }
        public List<BeatInfo> Redo()
        {
            if(CanRedo)
            {
                undoPtr = undoPtr == null ? undoStack.First : undoPtr.Next;
                //if it's last redo note, we need to restore from newestRecord
                return CanRedo ? undoPtr.Next.Value.Value : newestRecord;
            }
            return null;
        }

        List<BeatInfo> GetClone(List<BeatInfo> beats)
        {
            List<BeatInfo> newList = new List<BeatInfo>(beats.Count);
            beats.ForEach((item) =>
            {
                newList.Add(item.Clone() as BeatInfo);
            });
            return newList;
        }
    }
}
