import Select, { components } from 'react-select';
import React, { useRef } from 'react';
import './searchByTagBar.css';

const MultiValueContainer = props => {
    return <components.MultiValueContainer {...props} />
};

function SearchSelect (props) {
    const options = props.allTags.map(tag => ({ value: tag, label: tag }));
    const searchSelect = useRef(null);

    const searchModeCheckboxOnChange = () => {
        props.changeSearchMode(searchSelect.current.state.value);
    };

    return (
    <div className="searchByTagBar">
        <Select
            ref={searchSelect}
            className="searchSelect border border-warning rounded"
            closeMenuOnSelect={false}
            components={{ MultiValueContainer }}
            isMulti
            options={options}
            onChange={props.searchByTags}
            placeholder="Select tags to search by" />
        <div className="form-check">
            <input className="form-check-input" type="checkbox" value="" onChange={searchModeCheckboxOnChange} />
            <label className="form-check-label">
                match any
            </label>
        </div>

    </div>);
}

export default SearchSelect;