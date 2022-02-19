using System.Collections;
using UnityEngine;

namespace Game.Tiles
{
    public class TileAnimator : MonoBehaviour
    {
        [SerializeField] private TileGenerator tileGenerator;
        private Transform player;

        private void Start()
        {
            tileGenerator.TileSpawned += OnTileSpawned;
        }

        public void SetPlayer(Transform player)
        {
            this.player = player;
        }

        private void OnTileSpawned(Transform tile)
        {
            StartCoroutine(WaitForPlayerPass(tile));
        }

        private IEnumerator WaitForPlayerPass(Transform tile)
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitUntil(() => Vector3.Distance(player.position, tile.position) < 2);
            yield return new WaitUntil(() => Vector3.Distance(player.position, tile.position) > 3);

            StartCoroutine(DropTile(tile));
        }

        private IEnumerator DropTile(Transform tile)
        {
            while (tile.localPosition.y > -20)
            {
                tile.localPosition += Vector3.down * Time.deltaTime * 12f;
                yield return null;
            }
        }
    }
}