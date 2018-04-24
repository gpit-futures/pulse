import React from 'react';
import renderer from 'react-test-renderer';

import PTButton from '../PTButton';

describe('Component <PTButton />', () => {
  it('should renders with props correctly', () => {
    const tree = renderer
      .create(
        <PTButton
          className="test-button"
          testProps="testProps"
        >test button</PTButton>)
      .toJSON();
    expect(tree).toMatchSnapshot();
  });

  it('should renders with children element correctly', () => {
    const anotherProps = 'anotherProps'
    const tree = renderer
      .create(
        <PTButton
          className="test-button"
          testProps="testProps"
          anotherProps={anotherProps}
        >
          <i className="btn-icon fa fa-angle-left"></i>
          <span className="btn-text">test text</span>
        </PTButton>)
      .toJSON();
    expect(tree).toMatchSnapshot();
  });
});