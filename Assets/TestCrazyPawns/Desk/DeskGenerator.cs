using UnityEngine;

namespace TestCrazyPawns.Desk
{
    public class DeskGenerator : MonoBehaviour
    {
        [SerializeField] private PawnsDesk deskPrefab;
        [SerializeField] private DeskCell cellPrefab;

        public PawnsDesk Generate(DeskConfigData data)
        {
            var deskInstance = Instantiate(deskPrefab);
            var xOffset = -0.5f * data.DeskSize.x * data.CellSize.x + 0.5f * data.CellSize.x;
            var yOffset = -0.5f * data.DeskSize.y * data.CellSize.y + 0.5f * data.CellSize.y;

            for (int y = 0; y < data.DeskSize.y; y++)
            {
                for (int x = 0; x < data.DeskSize.x; x++)
                {
                    var cell = Instantiate(cellPrefab, deskInstance.Root);
                    cell.Position = new Vector2(xOffset + x * data.CellSize.x,
                        yOffset + y * data.CellSize.y);
                    cell.Size = data.CellSize;

                    if (!IsEvenNumber(x) & !IsEvenNumber(y) | IsEvenNumber(x) & IsEvenNumber(y))
                    {
                        cell.CellColor = data.BlackCelColor;
                    }
                    else
                    {
                        cell.CellColor = data.WhiteCelColor;
                    }
                }
            }

            return deskInstance;
        }

        private bool IsEvenNumber(int number)
        {
            return number % 2 == 0;
        }
    }
}