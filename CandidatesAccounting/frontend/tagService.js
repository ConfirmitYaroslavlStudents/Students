import {fetchGet, fetchPost} from './fetcher';

export function getTags() {
  return fetchGet('/tags')
    .then((tags) => {
      let tagsArray = [];
      for (let i = 0; i < tags.length; i++) {
        tagsArray.push(tags[i]);
      }
      return tagsArray;
    })
}

export function addTag(tag) {
  return fetchPost('/tags', tag);
}