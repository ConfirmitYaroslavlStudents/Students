import {fetchGet} from '../components/common/fetch';

export function getTags() {
  return fetchGet('/api/tags')
    .then((tags) => {
      let tagsArray = [];
      for (let i = 0; i < tags.length; i++) {
        tagsArray.push(tags[i]);
      }
      return tagsArray;
    })
}