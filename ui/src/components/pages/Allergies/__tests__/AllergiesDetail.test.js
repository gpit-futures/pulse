import React from 'react';
import Enzyme, { shallow } from 'enzyme';
import Adapter from 'enzyme-adapter-react-15';

import AllergiesDetail from '../AllergiesDetail/AllergiesDetail';
import { valuesNames, valuesLabels } from '../forms.config';
import { getDDMMMYYYY } from '../../../../utils/time-helpers.utils';

Enzyme.configure({ adapter: new Adapter() });

const propsForAllergiePanel = {
  detail: {
    [valuesNames.CAUSE]: '1111',
    [valuesNames.CAUSECODE]: '1111',
    [valuesNames.TERMINOLOGY]: '1239085',
    [valuesNames.TERMINOLOGYCODE]: '1111',
    [valuesNames.REACTION]: '1111',
    [valuesNames.AUTHOR]: 'Dr Tony Shannon',
    [valuesNames.DATE_CREATED]: 1507020019000,
    [valuesNames.SOURCE]: 'ethercis',
    [valuesNames.SOURCE_ID]: '7ae34463-0770-4a36-bbde-4869bb6a0f05',
    [valuesNames.ORIGINAL_COMPOSITION]: '',
    [valuesNames.ORIGINAL_SOURCE]: '',
  },
};

const ALLERGIE_PANEL = 'allergiePanel';
const META_PANEL = 'metaPanel';
const CONVERT_DATE = getDDMMMYYYY(propsForAllergiePanel.detail[valuesNames.DATE_CREATED]);

describe('Component <AllergiesDetail />', () => {
  it('should renders with props correctly', () => {
    const component = shallow(<AllergiesDetail />);

    // Testing component when detail filled object, expandedPanel is all, and panel not edited
    component.setProps({ detail: propsForAllergiePanel.detail, expandedPanel: 'all', editedPanel: { [ALLERGIE_PANEL]: false } });
    expect(component.props().className).toEqual('section-detail');
    expect(component.find('PluginDetailPanel')).toHaveLength(2);

    // Testing allergiePanel
    expect(component.find('PluginDetailPanel').at(0).props().name).toEqual(ALLERGIE_PANEL);
    expect(component.find('PluginDetailPanel').at(0).props().title).toEqual('Allergy');
    expect(component.find('PluginDetailPanel').at(0).props().isOpen).toEqual(false);
    expect(component.find('PluginDetailPanel').at(0).props().isBtnShowPanel).toEqual(true);
    expect(component.find('PluginDetailPanel').at(0).props().isShowControlPanel).toEqual(true);

    expect(component.find('.control-label').at(0).text()).toEqual(valuesLabels.CAUSE);
    expect(component.find('.control-label').at(1).text()).toEqual(valuesLabels.REACTION);
    expect(component.find('.control-label').at(2).text()).toEqual(valuesLabels.AUTHOR);
    expect(component.find('.control-label').at(3).text()).toEqual(valuesLabels.DATE_CREATED);
    expect(component.find('.control-label').at(4).text()).toEqual(valuesLabels.SOURCE);

    expect(component.find('.form-control-static').at(0).text()).toEqual(propsForAllergiePanel.detail.cause);
    expect(component.find('.form-control-static').at(1).text()).toEqual(propsForAllergiePanel.detail.causeCode);
    expect(component.find('.form-control-static').at(2).text()).toEqual(propsForAllergiePanel.detail.author);
    expect(component.find('.form-control-static').at(3).text()).toEqual(CONVERT_DATE);
    expect(component.find('.form-control-static').at(4).text()).toEqual(propsForAllergiePanel.detail.source);

    // Testing metaPanel
    expect(component.find('PluginDetailPanel').at(1).props().name).toEqual(META_PANEL);
    expect(component.find('PluginDetailPanel').at(1).props().title).toEqual('Metadata');
    expect(component.find('PluginDetailPanel').at(1).props().isOpen).toEqual(false);
    expect(component.find('PluginDetailPanel').at(1).props().isBtnShowPanel).toEqual(true);
    expect(component.find('PluginDetailPanel').at(1).props().isShowControlPanel).toEqual(false);

    expect(component.find('.control-label').at(5).text()).toEqual(valuesLabels.CAUSECODE);
    expect(component.find('.control-label').at(6).text()).toEqual(valuesLabels.TERMINOLOGY);

    expect(component.find('.form-control-static').at(5).text()).toEqual(propsForAllergiePanel.detail.causeCode);
    expect(component.find('.form-control-static').at(6).text()).toEqual(propsForAllergiePanel.detail.causeTerminology);
    expect(component).toMatchSnapshot();

    component.setProps({ detail: { [valuesNames.ISIMPORT]: true } });
    expect(component.find('.form-control-static').at(5).text()).toEqual(propsForAllergiePanel.detail[valuesNames.ORIGINAL_SOURCE]);
    expect(component.find('.control-label').at(5).text()).toEqual(valuesLabels.ORIGINAL_SOURCE);
    expect(component.find('.control-label').at(6).text()).toEqual(valuesLabels.ISIMPORT);
    expect(component.find('Switch')).toHaveLength(1);
  });

  it('should renders correctly with different state of props', () => {
    const component = shallow(
      <AllergiesDetail />);
    // Testing component when detail empty object, expandedPanel is allergiePanel
    component.setProps({ detail: { [valuesNames.DATE_CREATED]: 1507020019000 }, expandedPanel: ALLERGIE_PANEL, editedPanel: { [ALLERGIE_PANEL]: false } });
    expect(component.find('PluginDetailPanel')).toHaveLength(1);
    expect(component.find('PluginDetailPanel').props().name).toEqual(ALLERGIE_PANEL);
    expect(component).toMatchSnapshot();

    // Testing component when detail empty object, expandedPanel is metaPanel
    component.setProps({ detail: {}, expandedPanel: META_PANEL });
    expect(component.find('PluginDetailPanel')).toHaveLength(1);
    expect(component.find('PluginDetailPanel').props().name).toEqual(META_PANEL);
    expect(component).toMatchSnapshot();

    // Testing component when detail filled object, expandedPanel is all, and panel is edited
    component.setProps({ detail: propsForAllergiePanel.detail, expandedPanel: ALLERGIE_PANEL, editedPanel: { [ALLERGIE_PANEL]: true } });
    expect(component.find('ReduxForm')).toHaveLength(1);
    expect(component.find('PluginDetailPanel').props().name).toEqual(ALLERGIE_PANEL);
    expect(component).toMatchSnapshot();
  });
});

